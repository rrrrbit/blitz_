using UnityEngine;

public class BG_lattice : GAME_obj
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().size = new(Random.Range(5, 20), Random.Range(5, 20));
    }
}
