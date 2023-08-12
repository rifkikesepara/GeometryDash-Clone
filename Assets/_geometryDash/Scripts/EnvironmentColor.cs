using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentColor : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public float colorChangerCoefficent;
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        foreach (var sprite in sprites)
        {
            sprite.color = new Color((MathF.Sin(Time.time / 2) + 1) / 2 + 0.2f + colorChangerCoefficent,
                (MathF.Sin(Time.time / 4) + 1) / 2 + 0.2f + colorChangerCoefficent,
                (MathF.Sin(Time.time / 10) + 1) / 2 + 0.2f + colorChangerCoefficent,
                1);
        }
    }
}
