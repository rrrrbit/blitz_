using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using System.Collections;

public class GAME_spawns : MonoBehaviour
{
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
		objInst.GetComponent<GAME_obj>().Spawn(spawn);
    }

	public void QueueSpawn(QueuedSpawn spawn)
	{
		spawnQueue.Add(spawn);
	}

	public struct QueuedSpawn
	{
		public QueuedSpawn(Transform Origin, Vector3 Offset, Dictionary<GameObject, int> PossibleObjs)
		{
			origin = Origin;
			offset = Offset;
			possibleObjs = PossibleObjs;
		}
		
		public Vector3 AbsPos() => origin.transform.position + offset;

		public Transform origin;
		public Vector3 offset;
		public Dictionary<GameObject, int> possibleObjs;
	}

    private void Start()
    {
		mvt = player.GetComponent<PLAYER_baseMvt>();

		Invoke("StartDelayed", 0.01f);
    }

	void StartDelayed()
	{
        foreach (var obj in objs)
        {
            obj.GetComponent<GAME_obj>().SpawnStart();
        }	
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
        }

        Debug.DrawLine(new Vector3(deleteThreshhold, 100, 0), new Vector3(deleteThreshhold, -100, 0), Color.red);
		Debug.DrawLine(new Vector3(start, 100, 0), new Vector3(start, -100, 0), Color.green);
		Debug.DrawLine(new Vector3(nearest.AbsPos().x, 100, 0), new Vector3(nearest.AbsPos().x, -100, 0), Color.blue);

    }

}
