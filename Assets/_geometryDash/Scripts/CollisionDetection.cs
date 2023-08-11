using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.transform.tag)
        {
            case "Obstacle":LevelManager.Instance.died = true;
                Destroy(gameObject);
                break;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.transform.tag)
        {
            case "Fly": LevelManager.Instance.gameMode = LevelManager.GameMode.Fly;
                break;
            case "Jump": LevelManager.Instance.gameMode = LevelManager.GameMode.Jump;
                break;
        }
    }
}
