using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GAME_manager : MonoBehaviour
{

    public int score = 0;
    public float baseSpeed = 40;
    public float speedMult = 0;
    public float speed = 1;

    public List<GameObject> interactables = new();

    void Update()
    {
        speedMult = GLOBAL.Lerpd(speedMult, 1, .5f, .5f, Time.deltaTime);
        speed = baseSpeed * speedMult;
		baseSpeed += 1f / 6 * Time.deltaTime;
    }

    public void AddScore(int amt)
    {
        score += amt;
    }
}
