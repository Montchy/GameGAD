using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float wallJumpForce = 10f;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    private Camera playerCamera;
    private Rigidbody rb;
    public bool isGrounded = false;
    public bool isTouchingWall = false;
    private bool canWallJump = true;
    public GameObject topBody;
    public bool isCrouching = false;

    public bool sprint = false;
    public float sprintspeed = 2f;

    private Vector3 wallNormal;
    private Collider lastWall;

    public float crouchAmount = 1f; // Amount to crouch down or stand up
    public float crouchSpeed = 0.5f; // Speed of crouching/standing movement

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.freezeRotation = true;
    }

    void Update()
    {
        MovePlayer();
        LookAround();

        if (isGrounded && Input.GetKey(KeyCode.Space) && !isCrouching)
        {
            Jump();
        }
        else if (isTouchingWall && canWallJump && Input.GetKey(KeyCode.Space))
        {
            WallJump();
            canWallJump = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            StartCoroutine(MoveTopBodyDownOrUp());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)){

            sprint = true;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            
            sprint = false;
        }

    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(!sprint){

        Vector3 moveDirection = playerCamera.transform.right * horizontal + playerCamera.transform.forward * vertical;
        moveDirection.y = 0f;

        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);

        }
        else if(sprint){
        Vector3 moveDirection = playerCamera.transform.right * horizontal + playerCamera.transform.forward * vertical;
        moveDirection.y = 0f;

        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime*sprintspeed);
        }

        
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void WallJump()
    {
        Vector3 jumpDirection = Vector3.up + wallNormal;
        jumpDirection.Normalize();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rb.AddForce(jumpDirection * wallJumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canWallJump = true;
        }
        if (collision.gameObject.CompareTag("WallJump"))
        {
            isTouchingWall = true;
            wallNormal = collision.contacts[0].normal;
            lastWall = collision.collider;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("WallJump"))
        {
            if (collision.collider == lastWall)
            {
                isTouchingWall = false;
                lastWall = null;
            }
        }
    }

    private IEnumerator MoveTopBodyDownOrUp()
    {
        Vector3 startPosition = topBody.transform.localPosition;
        Vector3 targetPosition = isCrouching 
            ? startPosition - new Vector3(0, crouchAmount, 0) 
            : startPosition + new Vector3(0, crouchAmount, 0);

        float elapsedTime = 0f;

        while (elapsedTime < crouchSpeed)
        {
            float progress = elapsedTime / crouchSpeed;
            topBody.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        topBody.transform.localPosition = targetPosition; // Ensure final position is set
    }

    
}
