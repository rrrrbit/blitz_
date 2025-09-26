using UnityEngine;

public class OB_empty : GAME_obj
{
    public override void Spawn(GAME_spawns.QueuedSpawn ctx)
    {
        GAME.spawns.objs.Add(gameObject);
    }
}
