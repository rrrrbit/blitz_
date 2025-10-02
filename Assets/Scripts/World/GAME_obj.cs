using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class GAME_obj : MonoBehaviour
{
	public float length;
	public Collider2D bounds;
	
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
		bounds = GetComponent<Collider2D>();
	}

	//public virtual void OnDrawGizmos()
	//{
	//	Gizmos.color = Color.green;
	//	if (bounds)
	//	{

	//		Gizmos.DrawWireCube(bounds.bounds.center, bounds.bounds.size);
	//	}

	//}
}
