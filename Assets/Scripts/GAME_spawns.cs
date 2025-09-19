using UnityEngine;
using System.Collections.Generic;

public class GAME_spawns : MonoBehaviour
{
    public float deleteThreshhold;
    public float start;

    public float maxObjs;

    public GameObject[] objTypes;

    public List<GameObject> queue = new();

    public GameObject player;

    enum ObjTypeEnum
    {
        window
    }

    void Spawn()
    {
        
    }

    private void Start()
    {
        Instantiate(objTypes[(int)ObjTypeEnum.window]);
    }

    private void Update()
    {
        while (queue.Count > maxObjs)
        {
            Spawn();
        }
    }

}
