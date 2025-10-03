using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OBJ_window : TrajectoryAffectable
{
    public Vector2 size;
    public GameObject body;
	public GameObject contents;

    SpriteRenderer sprite;
	[SerializeField] Sprite[] sprites;
	bool hasIcon = false;

	public bool presentAtStart = false;

    public override IEnumerable<Trajectory> Trajectories()
    {
		return new List<Trajectory>(){
			new Trajectory(transform, new Vector2(size.x, 0), GAME.plyrMvt.JumpLength()),
            new Trajectory(transform, new Vector2(size.x + GAME.plyrMvt.JumpLength()/2, GAME.plyrMvt.jumpHeight), GAME.plyrMvt.JumpLength()/2)
        };
    }

	public override void SetBounds()
	{
        bounds = body.GetComponent<Collider2D>();
	}

    public override void Ready()
    {
        base.Ready();
        transform.position += new Vector3(-GAME.spawns.grace, -1);
		size += Vector2.right * GAME.spawns.grace;
        UpdateSize();
    }

    public void Start()
    {
		sprite = body.GetComponent<SpriteRenderer>();
		if (presentAtStart)
		{
			SetBounds();
			Ready();
		}


		
		var layer = Random.Range(-20000, 20000);
		sprite.sortingOrder = 2 * layer;
		contents.GetComponent<SpriteRenderer>().sortingOrder = 2 * layer + 1;

		UpdateSize();
    }

	public void UpdateSize()
	{
        contents.transform.localPosition = size / 2 * new Vector2(1, -1) + Vector2.down * 16 / 27f;
        contents.transform.localScale = Vector2.one * Mathf.Min(size.x, size.y) * .6f;
        sprite.size = size;
        body.GetComponent<BoxCollider2D>().size = size;
        body.transform.localPosition = size / 2 * new Vector2(1, -1);
        float aspectRatio = size.y / size.x;
        hasIcon = aspectRatio > 0.9f && aspectRatio < 1.2f;
        if (hasIcon)
        {
            contents.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }

	public void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new(1,1,0,0.5f);

        Gizmos.DrawCube(bounds.bounds.center, bounds.bounds.size);
		Gizmos.color = Color.blue;

        foreach (var j in GAME.spawns.objs.Where(x => bounds.bounds.Intersects(x.GetComponent<GAME_obj>().bounds.bounds)))
        {
            Gizmos.DrawWireCube(j.GetComponent<GAME_obj>().bounds.bounds.center, j.GetComponent<GAME_obj>().bounds.bounds.size);
        }

   //     foreach (var i in GAME.spawns.objs.Select(x => x.GetComponent<GAME_obj>()).ToList())
   //     {
            
			
			//if (i.bounds != null && GetComponent<GAME_obj>().bounds.bounds.Intersects(i.bounds.bounds))
   //         {
   //             Gizmos.DrawWireCube(i.bounds.bounds.center, i.bounds.bounds.size);
   //         }
   //     }
    }
}
