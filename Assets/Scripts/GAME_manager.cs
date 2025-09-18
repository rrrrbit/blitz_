using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GAME_manager : MonoBehaviour
{
    public static GAME_manager manager {  get; private set; }
    
    public int score = 0;
    public int speed = 50;

    public List<GameObject> interactables = new List<GameObject>();

    private void Awake()
    {
        if (manager == null) { manager = this; }
        else { Destroy(this); }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
