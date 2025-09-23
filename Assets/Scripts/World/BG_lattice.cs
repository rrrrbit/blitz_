using UnityEngine;

public class BG_lattice : GAME_obj
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void Spawn()
    {
        GetComponent<SpriteRenderer>().size = new(Random.Range(5, 20), Random.Range(5, 20));
    }
}
