using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OBJ_burst : TrajectoryAffectable, IInteractable
{
    [SerializeField] bool repeatable;
    [SerializeField] Transform inner;
    [SerializeField] Vector2 rotateSpeed;
    [SerializeField] float boostTime;

    public bool beenSlashed { get; set; }
    public void Slash(GameObject context)
    {
        PLAYER_baseMvt mvt = context.GetComponent<PLAYER_baseMvt>();

        if (mvt != null && (!beenSlashed || repeatable))
        {
            StartCoroutine(Burst(mvt));
        }

        beenSlashed = true;
    }

    public override IEnumerable<Trajectory> Trajectories()
    {
        return new List<Trajectory>(){
            new Trajectory(transform, new Vector2(GAME.mgr.speed * boostTime , 0), GAME.plyrMvt.JumpLength() * 1.5f)
        };
    }

    IEnumerator Burst(PLAYER_baseMvt mvt)
    {
        //mvt.gameObject.GetComponent<Rigidbody2D>().linearVelocityX += mvt.boostForce * .5f;
        GAME.mgr.speedMult += 1f;
        mvt.gravityMult = 0f;
        mvt.GetComponent<Rigidbody2D>().linearVelocityY = 0f;
        yield return new WaitForSeconds(boostTime - 0.05f);
        mvt.gravityMult = 1f;
        GAME.mgr.baseSpeed += 1;
        GAME.mgr.speedMult -= 1f;

    }

    void Update()
    {
        transform.eulerAngles += Vector3.forward * rotateSpeed.x * Time.deltaTime;
        inner.eulerAngles += Vector3.forward * rotateSpeed.y * Time.deltaTime;


    }


    public override void Start()
    {
        base.Start();
        length = bounds.bounds.size.x;
        transform.eulerAngles.Set(0, 0, Random.Range(0, 90));
        inner.eulerAngles.Set(0, 0, Random.Range(0, 60));
        GAME.mgr.interactables.Add(gameObject);
    }
}
