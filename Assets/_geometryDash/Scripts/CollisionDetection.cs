using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Obstacle"))
        {
            Debug.Log("çarptı");
        }
    }
}
