using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.transform.tag)
        {
            case "Obstacle":
                CameraController.Instance.ShakeTheCamera(0.3f);
                LevelManager.Instance.Died(gameObject);
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        LevelManager.Instance.ChangeTheGameMode(col.transform.tag);
    }
}
