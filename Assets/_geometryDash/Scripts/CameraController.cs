using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    private SpriteRenderer backgroundSprite;
    private float _startingPos;
    private float _lengthOfSprite;
    public CinemachineVirtualCamera[] gameCams;

    public float AmountOfParallax;

    private void Awake()
    {
        Instance = this;
        gameCams = GetComponentsInChildren<CinemachineVirtualCamera>();
    }

    void Start()
    {
        _startingPos = transform.position.x;
        backgroundSprite = GetComponentInChildren<SpriteRenderer>();
        _lengthOfSprite = backgroundSprite.size.x;
    }

    void Update()
    {
        Vector3 Position = transform.position;
        float Temp = Position.x * (1 - AmountOfParallax);
        float Distance = Position.x * AmountOfParallax;
        
        Vector3 NewPosition = new Vector3(_startingPos + Distance, transform.position.y, backgroundSprite.transform.position.z);

        backgroundSprite.transform.position = NewPosition;

        if (Temp > _startingPos + (_lengthOfSprite / 2))
        {
            _startingPos += _lengthOfSprite;
        }
    }

    public void ShakeTheCamera(float duration)
    {
        StartCoroutine(Shake(duration));
    }

    private IEnumerator Shake(float t)
    {
        foreach (var cam in gameCams)
        {
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.5f;
        }
        yield return new WaitForSeconds(t);
        foreach (var cam in gameCams)
        {
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
        }
        
    }
}
