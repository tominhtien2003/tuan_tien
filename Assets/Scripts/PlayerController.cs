using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeedOfWheel;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private GameInput gameInput;

    private Rigidbody rb;
    private Vector2 inputVector;
    private bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        gameInput.playerInputAction.Player.Move.started += OnMoveStarted;
        gameInput.playerInputAction.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        gameInput.playerInputAction.Player.Move.started -= OnMoveStarted;
        gameInput.playerInputAction.Player.Move.canceled -= OnMoveCanceled;
    }

    private void OnMoveStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        isMoving = true;
    }

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        isMoving = false;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        inputVector = gameInput.GetMovementInputVector();
        Vector3 moveDirection = transform.forward * inputVector.y;

        rb.velocity = moveDirection * moveSpeed;

        if (inputVector != Vector2.zero)
        {
            HandleAnimationWheel();

            Quaternion targetRotation = Quaternion.Euler(0f, inputVector.x * rotateSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * targetRotation, Time.deltaTime * rotateSpeed);
        }
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
}
