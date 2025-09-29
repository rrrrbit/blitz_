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

    public override void Start()
    {
        base.Start();
        bounds = GetComponent<Collider2D>().bounds;
        transform.eulerAngles.Set(0, 0, Random.Range(0, 90));
        GAME.mgr.interactables.Add(gameObject);
    }

    public override void Spawn(GAME_spawns.QueuedSpawn ctx)
	{
		var mvt = GAME.spawns.mvt;
        var randomOffset = mvt.JumpLength() * Random.Range(.5f, 1.5f);
        Vector3 offsV = new(randomOffset, mvt.Trajectory(0, randomOffset));

		GAME.spawns.QueueSpawn(new(transform, offsV, new()
		{
			{GAME.spawns.window, 7 },
			{GAME.spawns.relay, 2 },
			{GAME.spawns.burst, 1 }
		},
		Random.Range(5f, 30f)));
	}
}
