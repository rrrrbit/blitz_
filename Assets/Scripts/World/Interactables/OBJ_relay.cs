using System.Drawing;
using UnityEngine;

public class OBJ_relay : Interactable
{
    [SerializeField] bool repeatable;
    public override void Slash(GameObject context)
    {
        PLAYER_baseMvt mvt = context.GetComponent<PLAYER_baseMvt>();
        
        if (mvt != null && (!beenSlashed || repeatable))
        {
            mvt.Jump();
        }

        beenSlashed = true;
    }

	public override void Spawn()
	{
		var mvt = GAME.spawns.mvt;

		var jumpLength = mvt.jumpTime * GAME.mgr.speed;

		transform.position = GAME.spawns.spawnPos + GAME.spawns.nextSpawnOffs;

		var randomOffset = Random.Range(jumpLength / 2, jumpLength * 1.5f);
		var offsV = new Vector3(randomOffset, 4 * randomOffset / GAME.mgr.speed / mvt.jumpTime * mvt.jumpHeight * (1 - randomOffset / GAME.mgr.speed / mvt.jumpTime));
		GAME.spawns.nextSpawnOffs = offsV;

		GAME.spawns.queue.Insert(0, gameObject);
	}
}
