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

    public List<GameObject> queue = new();

    public GameObject player;

	public PLAYER_baseMvt mvt;

	public Vector2 spawnPos = new();

	public Vector2 nextSpawnOffs = new();

    enum ObjTypeEnum
    {
        window
    }

    void Spawn()
    {
		Instantiate(objTypes[Random.Range(0, objTypes.Length)]).GetComponent<GAME_obj>().Spawn();
		spawnPos.x = queue[0].transform.position.x + queue[0].GetComponent<GAME_obj>().length;
		spawnPos.y = queue[0].transform.position.y;
	}

    private void Start()
    {
        //queue.Add(Instantiate(objTypes[(int)ObjTypeEnum.window]));
		spawnPos.x = start;
		mvt = player.GetComponent<PLAYER_baseMvt>();

		foreach (var obj in queue)
		{
			obj.GetComponent<GAME_obj>().SpawnStart();
		}
    }

    private void Update()
    {
		spawnPos.x = queue[0].transform.position.x + queue[0].GetComponent<GAME_obj>().length;
		spawnPos.y = queue[0].transform.position.y;

		while (queue.Count < maxObjs && spawnPos.x < start)
        {
            Spawn();
        }
		//return;
		GLOBAL.DrawCross(spawnPos, 10, Color.green);

		Debug.DrawLine(spawnPos, spawnPos + nextSpawnOffs, Color.blue);

		GLOBAL.DrawCross(spawnPos + nextSpawnOffs);

		Debug.DrawLine(new Vector3(deleteThreshhold, 100, 0), new Vector3(deleteThreshhold, -100, 0), Color.red);
		Debug.DrawLine(new Vector3(start, 100, 0), new Vector3(start, -100, 0), Color.green);

	}

}
