using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    private Vector3 offset;
    private void Start()
    {
        offset = target.position - transform.position;
    }
    private void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = target.position - offset;
    }
}
