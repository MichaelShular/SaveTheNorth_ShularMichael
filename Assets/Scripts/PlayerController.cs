using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpForce = 5;
    private bool isJumping;
    private  Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
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

        if (moveDirection != Vector3.zero)
        {
            transform.forward = new Vector3(inputVector.x, 0, inputVector.y);
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
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !isJumping) return;
        isJumping = false;
    }
}
