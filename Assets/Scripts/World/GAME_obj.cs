using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class GAME_obj : MonoBehaviour
{
	public float length;
	public Bounds bounds;
	
	protected virtual void FixedUpdate()
    {

		GetComponent<Rigidbody2D>().linearVelocityX = -GAME.mgr.speed;

		//Gizmos.DrawWireCube(bounds.center, bounds.size);

        if (transform.position.x < GAME.spawns.deleteThreshhold)
        {
            GAME.spawns.objs.Remove(gameObject);
			if (GAME.mgr.interactables.Contains(gameObject))
			{
				GAME.mgr.interactables.Remove(gameObject);
			}
			Destroy(gameObject, 0.1f);
        }
    }

	public virtual void SetBounds()
	{

	}
}
