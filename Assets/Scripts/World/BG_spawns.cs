using System.Collections.Generic;
using UnityEngine;

public class BG_spawn : MonoBehaviour
{
    public float start;
    public int maxObjs;
    public GameObject objType;

    public Vector2 nxtSpwnDstBounds;

    public float nextSpawnDist;
    Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSpawnDist = Random.Range(nxtSpwnDstBounds.x, nxtSpwnDstBounds.y);
    }

    void Spawn()
    {
        var obj = Instantiate(objType);
        cam = GAME.spawns.mvt.cam;
        obj.transform.position = new(start, cam.transform.position.y + Random.Range(-50f, 50), obj.transform.position.z);

        obj.GetComponent<GAME_obj>().Spawn(new());
        
        nextSpawnDist = Random.Range(nxtSpwnDstBounds.x, nxtSpwnDstBounds.y);
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawnDist -= GAME.mgr.speed * Time.deltaTime;
        if (nextSpawnDist < 0)
        {
            Spawn();
        }
    }
}
