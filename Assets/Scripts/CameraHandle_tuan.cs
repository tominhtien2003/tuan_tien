using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraHandle_tuan : MonoBehaviour
{
    public Transform target;
    public Transform sniper;
    public Transform barrel;
    public Transform point;
    public GameObject cameraZoom;
    public Transform cameraPosition;
    public Transform center;

    public Vector3 offset;
    public Vector3 sniperOffset;
    private Vector3 previousPosition;

    public float rotationSpeed = 0.1f;
    private float barrelSpeed = 40f;

    private bool Zoomed;

    void Update()
    {
        if (!Zoomed)
        {
            FollowTarget();
        }
    }

    void LateUpdate()
    {
        RotateCamera();
        RotateBarrel();
    }

    private void FollowTarget()
    {
        transform.position = cameraPosition.position;
        transform.rotation = cameraPosition.rotation;
    }

    private void RotateCamera()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = previousPosition - Input.mousePosition;
                float rotationAroundYAxis = -direction.x * rotationSpeed * Time.deltaTime;

                cameraPosition.RotateAround(target.position, Vector3.up, rotationAroundYAxis);

                offset = cameraPosition.position - target.position;

                previousPosition = Input.mousePosition;
            }
        }
    }

    private void RotateBarrel()
    {
        Vector3 targetDirection = transform.forward;
        targetDirection.y = 0;

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            barrel.rotation = Quaternion.RotateTowards(barrel.rotation, targetRotation, barrelSpeed * Time.deltaTime);
        }
    }

    public void CameraZoom()
    {
        if (Zoomed)
        {
            transform.position = cameraPosition.position;
            Zoomed = false;
        }
        else
        {
            Debug.Log("zoomed");
            transform.position += transform.forward * 50f;
            Zoomed = true;
        }
    }


}
