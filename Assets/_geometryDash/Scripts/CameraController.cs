using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    private SpriteRenderer backgroundSprite;
    private float _startingPos;
    private float _lengthOfSprite;
    public CinemachineVirtualCamera gameCam;

    public float AmountOfParallax;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _startingPos = transform.position.x;
        backgroundSprite = GetComponentInChildren<SpriteRenderer>();
        _lengthOfSprite = backgroundSprite.size.x;
        gameCam = GetComponentInChildren<CinemachineVirtualCamera>();
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
}
