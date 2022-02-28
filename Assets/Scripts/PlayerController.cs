using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpForce = 5;
    public float maxNumberOfSnowballs;
    public float currentNumberOfSnowballs;
    private bool isJumping;
    private bool canShoot;
    private bool canCollect;
    private bool collectionButtonPress;
    private bool shootButtonPress;

    private GameObject gameUIController;

    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    private Rigidbody playerRigidbody;
    private GameObject currentFloorTouch;
    Animator playerAnimator;

    public AudioSource jumpSound;
    public GameObject snowball;
    public Transform snowballSpawnPos;
    public Slider snowBar;

    public readonly int isRunningHash = Animator.StringToHash("isRunning");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        canCollect = true;
        canShoot = true;
        isJumping = false;
        playerAnimator = GetComponentInChildren<Animator>();
        gameUIController = GameObject.Find("GameCanvas");
        snowBar.maxValue = maxNumberOfSnowballs;
        snowBar.value = currentNumberOfSnowballs;
    }

    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
        CollectionUpdate();
        ShootUpdate();
        //UI updates last 
        UIUpdate();
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
            playerAnimator.SetBool(isRunningHash, true);
        }
        else
        {
            playerAnimator.SetBool(isRunningHash, false);
        }
    }
    private void CollectionUpdate()
    {
        if(canCollect && collectionButtonPress && currentNumberOfSnowballs < maxNumberOfSnowballs)
        {
            canCollect = false;
            currentFloorTouch.GetComponent<FloorController>().DecreaseFloorSize();
            currentNumberOfSnowballs++;
            StartCoroutine(collectionCoolDown());
        }
    }
    private void ShootUpdate()
    {
        if(canShoot && shootButtonPress && currentNumberOfSnowballs > 0)
        {
            canShoot = false;
            var temp = Instantiate(snowball);
            temp.transform.position = snowballSpawnPos.position;
            temp.GetComponent<SnowballController>().moveDirection = transform.forward;
            currentNumberOfSnowballs--;
            StartCoroutine(shootCoolDown());
        }
    }

    private void UIUpdate()
    {
        snowBar.value = currentNumberOfSnowballs;
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
        playerAnimator.SetBool(isJumpingHash, true);
        jumpSound.Play();
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

    public void OnShoot(InputValue value)
    {
        shootButtonPress = value.isPressed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        currentFloorTouch = collision.gameObject;

        if (!isJumping) return;
        isJumping = false;
        playerAnimator.SetBool(isJumpingHash, false);

    }

    IEnumerator collectionCoolDown()
    {
        yield return new WaitForSeconds(5);
        canCollect = true;
    }

    IEnumerator shootCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
    public void RespawnPlayer()
    {
        var temp = GameObject.FindGameObjectWithTag("Ground");
        if (temp != null)
        {
            transform.position = temp.transform.parent.transform.parent.transform.position + Vector3.up * 5;
        }
        else
        {
            gameUIController.GetComponent<GameUIController>().openGameStateCanvas("No platforms left \n You lose");
        }
    }
}
