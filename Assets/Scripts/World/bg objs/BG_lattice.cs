using UnityEngine;

public class BG_lattice : GAME_obj
{
    public override void Spawn()
    {
        GetComponent<SpriteRenderer>().size = new(Random.Range(5, 20), Random.Range(5, 20));
    }
}
