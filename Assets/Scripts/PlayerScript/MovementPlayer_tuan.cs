using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovementPlayer_tuan : MonoBehaviour, IController_tuan
{
    private Rigidbody rb;
    private Vector2 moveDirection;
    [SerializeField] private List<Transform> Wheels;
    [SerializeField] private float wheelRotationSpeed;
    [SerializeField] private float tankRotateSpeed;

    private float speed;

    [SerializeField] private List<float> gearSpeed;
    private int currentSpeed;

    private void Start()
    {
        speed = gearSpeed[0];
        currentSpeed = 0;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Move();
        RotateTank();
    }

    private void Update()
    {
        RotateWheel();
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized;
    }

    public void Move()
    {
        if (moveDirection.y != 0)
        {
            Vector3 move = transform.forward * moveDirection.y;
            rb.velocity = move * speed;
        }
        else
        {
            SoundManager_tuan.instance.PlayAudio("Driver");
            rb.velocity = Vector3.zero;
        }
    }
    private void RotateWheel()
    {
        if (moveDirection != Vector2.zero)
        {
            foreach (var wheel in Wheels)
            {
                wheel.Rotate(wheelRotationSpeed * moveDirection.y * Time.deltaTime, 0f, 0f);
            }
        }
    }

    private void RotateTank()
    {
        if (moveDirection.x != 0)
        {
            transform.Rotate(0f, moveDirection.x / 2, 0f, Space.World);
        }
    }

    public void GearSpeed()
    {
        int index = currentSpeed % gearSpeed.Count;
        speed = gearSpeed[index];
        Debug.Log(speed);
        currentSpeed++;
    }
}
