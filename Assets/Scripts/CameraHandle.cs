using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Start()
    {
        
    }

    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = target.position + offset;
    }
}
