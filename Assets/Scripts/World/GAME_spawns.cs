using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using System.Collections;

public class GAME_spawns : MonoBehaviour
{
	/*
    REWRITE PLAN OVERVIEW

	-replace class Interactable with interface IInteractable and Destructible with IDestructible (extends IInteractable).

	-TrajectoryAffectable
		a class for objects that affect the platforming of the player.
		-bool solid
		-IEnumerable<Trajectory> Trajectories()
			returns all possible trajectories (jump/no jump, interact/no interact, etc.) from an object.
		-Start()
			add this objs trajectories to the list in GAME_spawns.

	-class Trajectory
		a class describing a trajectory (+x half of a parabola) on which an object can spawn.
		-Transform origin
		-Vector2 startPos
		-float maxDistX
			maximum distance an obj can spawn on this trajectory. it can still be evaluated outside this.
		-Vector3 Evaluate(float x)
		-float InverseEvaluate(float y)
			returns the x coordinate on this trajectory for a given y or null. 
			not ambiguous because this is only 1 half of the parabola.
		-bool CanLandOn(TrajectoryAffectable)
			checks whether the player can land on a given solid TrajectoryAffectable from this trajectory.
			does this by checking if the InverseEvaluate() of the top of the object is between the x-bounds of the object.
	
	-GAME_spawns:
		-List<Trajectory> allTrajectories
		-void Spawn()
			-select a random object
			-select only trajectories that start off camera. pick a random one from these.
			-if a platform, randomise the size of the object
			-evaluate along the Trajectory, reroll until it doesnt collide with any object+headroom.
			-place object.
			-remove every Trajectory that CanLandOn the new object from the list.
     */


	public float deleteThreshhold;
    public float start;
	public float grace;

    public int maxObjs;

    public GameObject[] objTypes;

	[Header("object type refs")]
	public GameObject window;
	public GameObject relay;
	public GameObject burst;
	public GameObject proxy;
	public GameObject kblitz;
	public GameObject node;
    public GameObject wall;
	public GameObject empty;
	[Space]

    public List<GameObject> objs = new();

	public List<QueuedSpawn> spawnQueue = new();

    public GameObject player;
	public PLAYER_baseMvt mvt;

	public List<Trajectory> allTrajectories = new();

	public QueuedSpawn nearest;

	void Spawn(QueuedSpawn spawn)
	{
		GameObject obj = null;
		int p = Random.Range(0, spawn.possibleObjs.Values.Sum());
		foreach (var item in spawn.possibleObjs)
		{
			if (p <= item.Value)
			{
				obj = item.Key;
				break;
			}
			p -= item.Value;
		}
		var objInst = Instantiate(obj);
		objInst.transform.position = spawn.origin.position + spawn.offset;
		objInst.GetComponent<GAME_obj>().length = spawn.platLength;
		objInst.GetComponent<GAME_obj>().Spawn(spawn);
    }

	public void QueueSpawn(QueuedSpawn spawn)
	{
		spawnQueue.Add(spawn);
	}

	public struct QueuedSpawn
	{
		public QueuedSpawn(Transform Origin, Vector3 Offset, Dictionary<GameObject, int> PossibleObjs, float PlatLength)
		{
			origin = Origin;
			offset = Offset;
			possibleObjs = PossibleObjs;
			platLength = PlatLength;
		}
		
		public Vector3 AbsPos() => origin.transform.position + offset;

		public Transform origin;
		public Vector3 offset;
		public Dictionary<GameObject, int> possibleObjs;
		public float platLength;
	}

    private void Start()
    {
		mvt = player.GetComponent<PLAYER_baseMvt>();
    }
	
    private void Update()
    {
		nearest = spawnQueue.OrderBy(x => x.origin.position.x + x.offset.x).First();


        if (objs.Count < maxObjs && nearest.AbsPos().x < start)
        {
            Spawn(nearest);
			spawnQueue.Remove(nearest);
            nearest = spawnQueue.OrderBy(x => x.origin.position.x + x.offset.x).First();
        }
        //return;

        foreach (var queued in spawnQueue)
		{
            GLOBAL.DrawCross(queued.origin.position + queued.offset);
            GLOBAL.DrawCross(queued.origin.position + queued.origin.gameObject.GetComponent<GAME_obj>().length * Vector3.right, 10, Color.blue);
			Debug.DrawLine(queued.origin.position + queued.offset, queued.origin.position + queued.origin.gameObject.GetComponent<GAME_obj>().length * Vector3.right, Color.purple);
			Debug.DrawLine(queued.origin.position + queued.offset, queued.origin.position + queued.offset + Vector3.right * queued.platLength, Color.green);
		}

        Debug.DrawLine(new Vector3(deleteThreshhold, 100, 0), new Vector3(deleteThreshhold, -100, 0), Color.red);
		Debug.DrawLine(new Vector3(start, 100, 0), new Vector3(start, -100, 0), Color.green);
		Debug.DrawLine(new Vector3(nearest.AbsPos().x, 100, 0), new Vector3(nearest.AbsPos().x, -100, 0), Color.blue);

    }

}
