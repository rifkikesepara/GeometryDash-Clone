using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameMode gameMode;
    private Transform spawnPoint;
    private TMP_Text attemptText;

    public float movementSpeed;
    public int attemptCounter = 1;
    public bool died = false;

    public GameObject Player;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameMode = GameMode.Jump;
        attemptText = GetComponentInChildren<TMP_Text>();
        foreach (var childObj in GetComponentsInChildren<Transform>())
        {
            if (childObj.CompareTag("Respawn"))
                spawnPoint = childObj;
        }

        CameraController.Instance.gameCam.Follow =
            Instantiate(Player, spawnPoint.position, spawnPoint.rotation).transform;
    }

    private void Update()
    {
        attemptText.text = "Attempt " + attemptCounter;
        if (died || Input.GetKeyDown(KeyCode.R))
        {
            died = false;
            attemptCounter++;
            CameraController.Instance.gameCam.Follow = Instantiate(Player, spawnPoint.position, spawnPoint.rotation).transform;
            CameraController.Instance.gameCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0.0f;
        }
    }
    public enum GameMode
    {
        Jump=0,Fly
    }
}

