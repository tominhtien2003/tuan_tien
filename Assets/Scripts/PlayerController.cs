using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeedOfWheel;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private Transform gunBarrel;
    [SerializeField] private GameInput gameInput;

    private Rigidbody rb;

    private bool isMoving;
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

    private void Update()
    {
        
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
        Vector2 inputVector = gameInput.GetMovementInputVectorNormalized();

        Vector3 moveDirection = transform.TransformDirection(new Vector3(inputVector.x, 0f, inputVector.y));

        if (moveDirection != Vector3.zero)
        {
            HandleAnimationWheel();

            transform.position += moveSpeed * moveDirection * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime);
        }

    }
    private void HandleRotateGun()
    {

    }
    private void OnDisable()
    {
        gameInput.playerInputAction.Player.Move.started -= Move_started;
        gameInput.playerInputAction.Player.Move.canceled -= Move_canceled;
    }
}
