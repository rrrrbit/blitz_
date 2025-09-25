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

		var jumpLength = mvt.jumpTime * GAME.mgr.baseSpeed;

		//transform.position = ctx.offset;

		var randomOffset = Random.Range(jumpLength / 2, jumpLength * 1.5f);
		var offsV = new Vector3(randomOffset, 4 * randomOffset / jumpLength * mvt.jumpHeight * (1 - randomOffset / jumpLength));

        GAME.spawns.QueueSpawn(new(transform, offsV, new()
        {
            {GAME.spawns.window, 4 },
            {GAME.spawns.relay, 2 },
            {GAME.spawns.burst, 1 }
        }));

        GAME.spawns.objs.Insert(0, gameObject);
	}
}
