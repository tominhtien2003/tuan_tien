using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform gunBarrel;
    [SerializeField] private float rotateSpeedGunBarrel;
    [SerializeField] float rotateSpeedCameraWhenSwipe = 0.1f;

    private float offsetDis;
    private Vector3 lastMousePosition;
    private bool isSwiping;
    private int type = 0;

    private void Start()
    {
        offsetDis = Vector3.Distance(target.position, transform.position);
    }
    private void LateUpdate()
    {
        HandleRotateWhenSwipe();
        RotateGunBarrel();
        UpdateCameraPosition();
    }
    private void HandleRotateWhenSwipe()
    {
        if (Input.GetMouseButton(0) && IsWithinSwipeArea())
        {
            Vector3 currentMousePosition = Input.mousePosition;

            if (lastMousePosition != Vector3.zero)
            {
                Vector3 mouseDelta = (currentMousePosition - lastMousePosition).normalized;

                float rotationAmount = mouseDelta.x * rotateSpeedCameraWhenSwipe * Time.deltaTime;

                target.Rotate(Vector3.up, rotationAmount);
            }

            lastMousePosition = currentMousePosition;

            isSwiping = true;
        }
        else
        {
            ResetSwipeState();
        }
    }
    public void ZoomViewfinder()
    {
        if (type == 0)
        {
            Camera.main.fieldOfView = 20;
        }
        else
        {
            Camera.main.fieldOfView = 60;
        }
        type = 1 - type;
    }
    private void UpdateCameraPosition()
    {
        if (target == null) return;

        Vector3 offset = -target.forward * offsetDis;

        offset.y = 6.7f;

        transform.position = target.position + offset;

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z), Vector3.up);
    }

    private bool IsWithinSwipeArea()
    {
        if (isSwiping)
        {
            return true;
        }
        GameObject obj = EventSystem.current.currentSelectedGameObject;

        if (obj != null && obj.CompareTag("AreaSwipeCamera"))
        {
            isSwiping = true;

            return true;
        }

        return false;
    }
    private void RotateGunBarrel()
    {
        if (target == null || gunBarrel == null) return;
        if (target.forward == gunBarrel.forward)
        {
            Debug.Log("Same direction");
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(target.forward);

        gunBarrel.rotation = Quaternion.RotateTowards(gunBarrel.rotation, targetRotation, rotateSpeedGunBarrel * Time.deltaTime);
    }
    private void ResetSwipeState()
    {
        isSwiping = false;

        lastMousePosition = Vector3.zero;
    }
}
