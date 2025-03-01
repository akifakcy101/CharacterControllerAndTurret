using UnityEngine;
using UnityEngine.Rendering;

public class FPSController : MonoBehaviour
{
    [Header("Referances")]
    private Camera playerCamera;
    private CharacterController characterController;

    [Header("Control Settings")]
    private float characterSpeed;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpPower = 5f;
    public float gravity = 9.8f;
    public bool isRunning = false;

    public float lookSens = 1f;
    public float lookXLimit = 60f;
    private float rotationX;

    public float moveDirectionX;
    public float moveDirectionZ;
    public float moveDirectionY;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        playerCamera = Camera.main;
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        CursorActions();

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 up = transform.TransformDirection(Vector3.up);

        moveDirectionX = Input.GetAxis("Vertical");
        moveDirectionZ = Input.GetAxis("Horizontal");

        //Çapraz yönlerde extra hýz yapmamasý için
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        //isrunning=Input.GetKey(KeyCode.LeftShift) yapýlabilir
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        
        //characterSpeed=isRunning?runSpeed : walkSpeed yapýlabilir
        if (isRunning)
        {
            characterSpeed = runSpeed;
        }
        else
        {
            characterSpeed = walkSpeed;
        }

        if (characterController.isGrounded)
        {
            moveDirectionY = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirectionY = jumpPower;
            }
        }
        else
        {
            moveDirectionY -= gravity * Time.deltaTime;
        }

        rotationX += -Input.GetAxis("Mouse Y") * lookSens;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSens, 0);

        moveDirection = (characterSpeed * moveDirectionX * forward) + (characterSpeed * moveDirectionZ * right) + (moveDirectionY * up);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private static void CursorActions()
    {
        //Cursor Açma-Kapama
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
