using UnityEngine;

public class BG_stripe : GAME_obj
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void Spawn()
    {
        GetComponent<SpriteRenderer>().size = new(Random.Range(1.5f, 12f), Random.Range(1.5f, 12f));

		transform.eulerAngles = new(0, 0, 45 * Random.Range(0,4));
    }
}
