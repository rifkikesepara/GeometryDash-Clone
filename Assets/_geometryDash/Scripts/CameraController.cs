using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public float offset = 0.0f;
    void Start()
    {
        
    }

    void Update()
    {
        mainCamera.transform.position =
            new Vector3(mainCamera.transform.position.x, offset, mainCamera.transform.position.z);
    }
}
