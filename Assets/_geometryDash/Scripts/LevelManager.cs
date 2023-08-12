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
    private int attemptCounter = 1;
    public bool died = false;
    public float DebugPosition;

    public GameObject dieParticle;
    public GameObject Player;
    public GameObject lastDeadObj;

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
            if (childObj.CompareTag("DeathPos"))
                lastDeadObj = childObj.gameObject;
        }

        Transform player = Instantiate(Player, spawnPoint.position, spawnPoint.rotation).transform;
        Debug.Log("rifki");

        foreach (var cam in CameraController.Instance.gameCams)
        {
            cam.Follow = player;
        }
    }

    private void Update()
    {
        attemptText.text = "Attempt " + attemptCounter;
    }

    public void Died(GameObject player)
    {
        died = true;
        attemptCounter++;
        StartCoroutine(Respawn(player.transform.position));
        lastDeadObj.transform.position = player.transform.position;
        foreach (var cam in CameraController.Instance.gameCams)
        {
            cam.Follow = lastDeadObj.transform;
        }
        Destroy(player);
    }

    private IEnumerator Respawn(Vector3 diePos)
    {
        Instantiate(dieParticle, diePos, quaternion.identity);
        yield return new WaitForSeconds(2);

        ChangeTheGameMode("Jump");
        
        Transform player = Instantiate(Player, spawnPoint.position, spawnPoint.rotation).transform;
        foreach (var cam in CameraController.Instance.gameCams)
        {
            cam.Follow = player;
            if (!cam.transform.CompareTag("Fly"))
                cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0.0f;
        }

        died = false;
    }

    public void ChangeTheGameMode(string mode)
    {
        switch (mode)
        {
            case "Fly": gameMode = GameMode.Fly;
                CameraController.Instance.gameCams[1].Priority = 100;
                break;
            case "Jump": gameMode = GameMode.Jump;
                CameraController.Instance.gameCams[1].Priority = -100;
                break;
            case "Finish": gameMode = GameMode.Finish;
                break;
        }
    }

    public enum GameMode
    {
        Jump=0,Fly,Finish
    }
}

