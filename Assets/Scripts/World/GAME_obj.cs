using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class GAME_obj : MonoBehaviour
{
	public GAME_objType typeEntry;
	public Collider2D bounds;
	
	protected virtual void FixedUpdate()
    {

		GetComponent<Rigidbody2D>().linearVelocityX = -GAME.mgr.speed;

        
    }

	protected virtual void Update()
	{
		if (transform.position.x < GAME.objMgr.deleteThreshhold)
		{
			DestroyCleanup();
		}
	}

	public void DestroyCleanup()
	{
		GAME.objMgr.objs.Remove(gameObject);
		GAME.mgr.interactables.Remove(gameObject);
		GAME.objMgr.unresolvedObjs.Remove(gameObject);
		GAME.objMgr.npObjs.Remove(gameObject);
		Destroy(gameObject);
	}

	public virtual void Ready() { }

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

public interface INode
{
	List<Transform> connections { get; set; }

	public float GetSmallestAngle();
}
