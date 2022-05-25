using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public Transform target;
    public float dampTime = 0.15f;
    private Vector3 _velocity = Vector3.zero;
    public Vector3 cameraPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        var position = target.position;
        cameraPos = new Vector3(position.x, position.y+2 , -10);
        // Interpola suavemente entre la posición actual de la cámara y su posición de destino.
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref _velocity, dampTime);
    }
}
