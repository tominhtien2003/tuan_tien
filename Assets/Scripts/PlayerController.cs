using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeedOfWheel;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private GameInput gameInput;

    private Rigidbody rb;

    public bool isMoving;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        gameInput.playerInputAction.Player.Move.started += Move_started;
        gameInput.playerInputAction.Player.Move.canceled += Move_canceled;
    }

    private void Move_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isMoving = false;
    }

    private void Move_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isMoving = true;
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleAnimationWheel()
    {
        if (isMoving)
        {
            foreach (var wheel in wheels)
            {
                wheel.Rotate(rotateSpeedOfWheel * Time.fixedDeltaTime, 0f, 0f);
            }
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementInputVector();

        Vector3 moveDirec = transform.forward * inputVector.y;

        if (inputVector.y != 0f)
        {
            HandleAnimationWheel();
        }

        rb.velocity = moveDirec * moveSpeed;

        if (inputVector != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, inputVector.x * rotateSpeed * Time.deltaTime, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * targetRotation, Time.deltaTime * rotateSpeed);
        }
    }
    private void OnDisable()
    {
        gameInput.playerInputAction.Player.Move.started -= Move_started;
        gameInput.playerInputAction.Player.Move.canceled -= Move_canceled;
    }
}
