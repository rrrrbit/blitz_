using UnityEngine;

public class OBJ_empty : GAME_obj
{
    private void Start()
    {
        GAME.objMgr.objs.Add(gameObject); 
    }
}
