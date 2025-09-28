using System.Drawing;
using UnityEngine;

public class OBJ_relay : Interactable
{
    [SerializeField] bool repeatable;
    [SerializeField] float rotateSpeed;
    public override void Slash(GameObject context)
    {
        PLAYER_baseMvt mvt = context.GetComponent<PLAYER_baseMvt>();
        
        if (mvt != null && (!beenSlashed || repeatable))
        {
            mvt.Jump();
        }

        beenSlashed = true;
    }

    void Update()
    {
        transform.eulerAngles += Vector3.forward * rotateSpeed * Time.deltaTime;


    }

    protected override void Start()
    {
        transform.eulerAngles.Set(0, 0, Random.Range(0, 90));
        base.Start();
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


		GAME.spawns.objs.Add(gameObject);
	}
}
