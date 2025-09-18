using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_slash : MonoBehaviour
{
    [SerializeField] Transform slashBoxPivot;
    [SerializeField] Collider2D slashBox;
    
    [SerializeField] float comboTime;

    float comboTImer;

    InputSystem_Actions.PlayerActions actions;

    void Slash()
    {
        Transform closest = null;
        float closestDistSqr = Mathf.Infinity;

        foreach(GameObject obj in GAME_manager.manager.interactables)
        {
            float distSqr = (obj.transform.position - transform.position).sqrMagnitude;
            if (distSqr < closestDistSqr)
            {
                closestDistSqr = distSqr;
                closest = obj.transform;
            }
        }


        slashBoxPivot.right = closest.position - transform.position;

        List<Collider2D> overlap = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        slashBox.Overlap(overlap);
    }

    void Setup()
    {
        actions = new InputSystem_Actions().player;
        actions.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (actions.slash.WasPressedThisFrame())
        {
            Slash();
        }
    }
}
