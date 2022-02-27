using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpForce = 5;
    private bool isJumping;
    private bool canCollect;
    private bool collectionButtonPress;

    private GameObject gameUIController;

    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    private Rigidbody playerRigidbody;
    private GameObject currentFloorTouch;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        canCollect = true;
        isJumping = false;

        gameUIController = GameObject.Find("GameCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
        CollectionUpdate();
    }
    private void MovementUpdate()
    {
        if (!(inputVector.magnitude > 0))
        {
            moveDirection = Vector3.zero;
        }

        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        Vector3 movementDirection = moveDirection * (movementSpeed * Time.deltaTime);
        transform.position += movementDirection;

        if (moveDirection != Vector3.zero && Time.timeScale == 1)
        {
            transform.forward = new Vector3(inputVector.x, 0, inputVector.y);
        }
    }
    private void CollectionUpdate()
    {
        if(canCollect && collectionButtonPress)
        {
            canCollect = false;
            currentFloorTouch.GetComponent<FloorController>().DecreaseFloorSize();
            StartCoroutine(collectionCoolDown());
        }
    }
    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
    public void OnJump(InputValue value)
    {
        if (isJumping) return;
        playerRigidbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }
    public void OnCollect(InputValue value)
    {
        collectionButtonPress = value.isPressed;
    }

    public void OnPause(InputValue value)
    {
        if(Time.timeScale == 1 && value.isPressed)
        {
            gameUIController.GetComponent<GameUIController>().pauseGame();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        currentFloorTouch = collision.gameObject;

        if (!isJumping) return;
        isJumping = false;
    }

    IEnumerator collectionCoolDown()
    {
        yield return new WaitForSeconds(5);
        canCollect = true;
    }

    public void RespawnPlayer()
    {
        var temp = GameObject.FindGameObjectWithTag("Ground");
        if (temp != null)
        {
            transform.position = temp.transform.position + Vector3.up * 5;
        }
        else
        {
            gameUIController.GetComponent<GameUIController>().openGameStateCanvas("No platforms left \n You lose");
        }
    }
}
