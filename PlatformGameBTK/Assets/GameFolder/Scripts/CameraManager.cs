using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float cameraSpeed,offsetX,offsetY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SmoothCamera();
    }
    void SmoothCamera()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x + offsetX, target.position.y + offsetY, transform.position.z), cameraSpeed);
    }
}
