using UnityEngine;

public class BG_lattice : GAME_obj
{
    public override void Spawn(GAME_spawns.QueuedSpawn ctx)
    {
        GetComponent<SpriteRenderer>().size = new(Random.Range(5, 20), Random.Range(5, 20));
    }
}
