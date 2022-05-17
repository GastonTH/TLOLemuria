using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public Transform target;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 cameraPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraPos = new Vector3(target.position.x, target.position.y , -10);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, dampTime);
    }
}
