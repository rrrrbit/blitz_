using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_slash : MonoBehaviour
{
    [SerializeField] Transform slashBoxPivot;
    [SerializeField] Collider2D slashBox;

    [SerializeField] float checkTime;
    [SerializeField] float comboTime;

    float checkTimer;
    float comboTImer;

    bool checking;

    InputSystem_Actions.PlayerActions actions;

    void Slash()
    {
        Transform closest = null;
        float closestDistSqr = Mathf.Infinity;

        foreach(GameObject obj in GAME.mgr.interactables)
        {
            float distSqr = (obj.transform.position - transform.position).sqrMagnitude;
            if (distSqr < closestDistSqr)
            {
                closestDistSqr = distSqr;
                closest = obj.transform;
            }
        }


        slashBoxPivot.right = closest.position - transform.position;

        checkTimer = checkTime;
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

        if (checkTimer > 0)
        {
            slashBox.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, .5f);

            List<Collider2D> overlap = new List<Collider2D>();
            slashBox.Overlap(overlap);
            foreach (Collider2D obj in overlap)
            {
                IInteractable interactable = obj.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Slash(gameObject);
                    if (interactable.GetType() == typeof(Destructible))
                    {
                        checkTimer = checkTime;
                    }
                }
            }

            checkTimer = Mathf.Max(0, checkTimer - Time.deltaTime);
        }
    }
}
