using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class GAME_obj : MonoBehaviour
{
	public float length;
	
	protected virtual void FixedUpdate()
    {

		GetComponent<Rigidbody2D>().linearVelocityX = -GAME.mgr.speed;

        if (transform.position.x < GAME.spawns.deleteThreshhold)
        {
            GAME.spawns.queue.Remove(gameObject);
			if (GAME.mgr.interactables.Contains(gameObject))
			{
				GAME.mgr.interactables.Remove(gameObject);
			}
			Destroy(gameObject);
        }
    }

	public virtual void Spawn()
	{
		
	}

	public virtual void SpawnStart()
	{

	}
}
