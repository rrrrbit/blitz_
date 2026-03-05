using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLAYER_slash : MonoBehaviour
{
    [SerializeField] Transform slashBoxPivot;
    [SerializeField] Collider2D slashBox;
    [SerializeField] Animator slashBoxAnim;

    [SerializeField] float checkTime;
    [SerializeField] float comboTime;

    float checkTimer;
    float comboTImer;

    bool checking;
    bool flipAnim;

	public TOKEN_ControlScheme tokenControlScheme;

	InputSystem_Actions actions;
	InputSystem_Actions.PlayerActions plyrActions;

    void Slash()
    {
        Transform closest = null;
        float closestDistSqr = Mathf.Infinity;

        foreach(GameObject obj in GAME.objMgr.interactables)
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
        slashBoxAnim.Play("slash", -1, 0f);
        slashBox.GetComponent<SpriteRenderer>().flipY = flipAnim;
        flipAnim = !flipAnim;
    }

    void Setup()
    {
        actions = new InputSystem_Actions();
		actions.bindingMask = InputBinding.MaskByGroup(tokenControlScheme.GetScheme(actions).bindingGroup);
		plyrActions = actions.player;
        plyrActions.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (plyrActions.slash.WasPressedThisFrame())
        {
            Slash();
        }

        if (checkTimer > 0)
        {

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
