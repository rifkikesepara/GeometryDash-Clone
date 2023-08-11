using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject playerContainer;
    public float movementSpeed;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 pos = playerContainer.transform.position;
        playerContainer.transform.position = new Vector3(pos.x + movementSpeed, pos.y, pos.z);
    }
}
