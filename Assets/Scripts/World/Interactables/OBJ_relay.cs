using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class OBJ_relay : TrajectoryAffectable, IInteractable
{
    [SerializeField] bool repeatable;
    [SerializeField] float rotateSpeed;
    public bool beenSlashed { get; set; }
    public void Slash(GameObject context)
    {
        PLAYER_baseMvt mvt = context.GetComponent<PLAYER_baseMvt>();
        
        if (mvt != null && (!beenSlashed || repeatable))
        {
            mvt.Jump();
        }

        beenSlashed = true;
    }

    public override IEnumerable<Trajectory> Trajectories()
    {
        return new List<Trajectory>(){
            new Trajectory(transform, new Vector2(GAME.plyrMvt.JumpLength()/2, GAME.plyrMvt.jumpHeight), GAME.plyrMvt.JumpLength() * 1.5f)
        };
    }

    void Update()
    {
        transform.eulerAngles += Vector3.forward * rotateSpeed * Time.deltaTime;


    }
    public override void SetBounds()
    {
        bounds = GetComponent<Collider2D>().bounds;
    }

    public override void Start()
    {
        base.Start();
        length = bounds.size.x;
        transform.eulerAngles.Set(0, 0, Random.Range(0, 90));
        GAME.mgr.interactables.Add(gameObject);
    }
}
