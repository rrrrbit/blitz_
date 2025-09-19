using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GAME_manager : MonoBehaviour
{
    public static GAME_manager manager {  get; private set; }
    
    public GAME_spawns spawns { get; private set; }

    public int score = 0;
    public float baseSpeed = 40;
    public float speedMult = 0;
    public float speed;

    public List<GameObject> interactables = new List<GameObject>();

    private void Awake()
    {
        if (manager == null) { manager = this; }
        else { Destroy(this); }

    }

    private void Start()
    {
        spawns = GetComponent<GAME_spawns>();
    }

    void Update()
    {
        speedMult = GLOBAL.Lerpd(speedMult, 1, .5f, .75f, Time.deltaTime);
        speed = baseSpeed * speedMult;
    }
}
