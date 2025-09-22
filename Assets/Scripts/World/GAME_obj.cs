using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class GAME_obj : MonoBehaviour
{
	public float length;
	
	protected virtual void FixedUpdate()
    {
        transform.position += Vector3.left * GAME.mgr.speed * Time.fixedDeltaTime;

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
