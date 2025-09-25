using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;

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
	[Space]

    public List<GameObject> objs = new();

	public Queue<QueuedSpawn> spawnQueue = new();

    public GameObject player;
	public PLAYER_baseMvt mvt;

	public float furthest = 0f;

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
		spawnQueue.Enqueue(spawn);
	}

	public struct QueuedSpawn
	{
		public QueuedSpawn(Transform Origin, Vector3 Offset, Dictionary<GameObject, int> PossibleObjs)
		{
			origin = Origin;
			offset = Offset;
			possibleObjs = PossibleObjs;
		}
		
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

		furthest = objs.ConvertAll(x => x.transform.position.x + x.GetComponent<GAME_obj>().length).Max();
		

		if (objs.Count < maxObjs && furthest < start)
        {
			Spawn(spawnQueue.Dequeue());
            furthest = objs.ConvertAll(x => x.transform.position.x + x.GetComponent<GAME_obj>().length).Max();
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
		Debug.DrawLine(new Vector3(furthest, 100, 0), new Vector3(furthest, -100, 0), Color.blue);

    }

}
