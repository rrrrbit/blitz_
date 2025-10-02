using UnityEngine;

public class OB_empty : GAME_obj
{
    private void Start()
    {
        GAME.spawns.objs.Add(gameObject); 
    }
}
