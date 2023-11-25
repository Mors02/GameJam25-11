
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 1f;
    public float cameraDistance = 100f;
    //public Vector3 offset;

    void LateUpdate()
    {
        //Vector3 desiredPos =  target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        transform.position = new Vector3(smoothPos.x, smoothPos.y, -cameraDistance);

    }
}
