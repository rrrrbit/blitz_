using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class OBJ_relay : TrajectoryAffectable, IInteractable
{
    [SerializeField] bool repeatable;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject particle;
    public bool beenSlashed { get; set; }
    public void Slash(GameObject context)
    {
        PLAYER_baseMvt mvt = context.GetComponent<PLAYER_baseMvt>();
        
        if (mvt != null && (!beenSlashed || repeatable))
        {
            mvt.Jump();
            var p = Instantiate(particle, context.transform);
            p.transform.localPosition = Vector3.zero;

        }

        beenSlashed = true;
    }

    public override IEnumerable<Trajectory> Trajectories()
    {
        return new List<Trajectory>(){
            new Trajectory(transform, new Vector2(GAME.plyrMvt.JumpLength()/2, GAME.plyrMvt.jumpHeight), GAME.plyrMvt.JumpLength()/2)
        };
    }

	protected override void Update()
	{
		base.Update();
		transform.eulerAngles += Vector3.forward * rotateSpeed * Time.deltaTime;


    }

	public override void Ready()
	{
		GAME.mgr.interactables.Add(gameObject);
	}

    public void Start()
    {
        transform.eulerAngles.Set(0, 0, Random.Range(0, 90));
        
    }
}
