using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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


	
	public Vector2 spawnPos = new();

	public float furthest = 0f;

	public Vector2 nextSpawnOffs = new();

    enum ObjTypeEnum
    {
        window
    }

    void Spawn()
    {
		Instantiate(objTypes[Random.Range(0, objTypes.Length)]).GetComponent<GAME_obj>().Spawn();
		spawnPos.x = objs[0].transform.position.x + objs[0].GetComponent<GAME_obj>().length;
		spawnPos.y = objs[0].transform.position.y;
	}

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
		objInst.GetComponent<GAME_obj>().Spawn();
		objInst.transform.position = (Vector2)spawn.origin.position + spawn.pos;
    }

	public void QueueSpawn(QueuedSpawn spawn)
	{
		spawnQueue.Enqueue(spawn);
	}

	public class QueuedSpawn
	{
		public Transform origin;
		public Vector2 pos;
		public Dictionary<GameObject, int> possibleObjs = new();
	}

    private void Start()
    {
        //queue.Add(Instantiate(objTypes[(int)ObjTypeEnum.window]));
		spawnPos.x = start;
		mvt = player.GetComponent<PLAYER_baseMvt>();

		foreach (var obj in objs)
		{
			obj.GetComponent<GAME_obj>().SpawnStart();
		}
    }

    private void Update()
    {
		foreach(var queued in spawnQueue)
		{
			queued.pos += Vector2.left * GAME.mgr.speed * Time.deltaTime;
		}

		furthest = objs.ConvertAll(x => x.transform.position.x + x.GetComponent<GAME_obj>().length).Max();
		
		spawnPos.x = objs[0].transform.position.x + objs[0].GetComponent<GAME_obj>().length;
		spawnPos.y = objs[0].transform.position.y;

		while (objs.Count < maxObjs && furthest < start)
        {
            //Spawn();

			Spawn(spawnQueue.Dequeue());
        }
		//return;

		Debug.DrawLine(spawnPos, spawnPos + nextSpawnOffs, Color.blue);

		GLOBAL.DrawCross(spawnPos, 10, Color.green);
		GLOBAL.DrawCross(spawnPos + nextSpawnOffs);

		foreach (var queued in spawnQueue)
		{
            GLOBAL.DrawCross(queued.pos);
        }

		Debug.DrawLine(new Vector3(deleteThreshhold, 100, 0), new Vector3(deleteThreshhold, -100, 0), Color.red);
		Debug.DrawLine(new Vector3(start, 100, 0), new Vector3(start, -100, 0), Color.green);
		Debug.DrawLine(new Vector3(furthest, 100, 0), new Vector3(furthest, -100, 0), Color.blue);

    }

}
