using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class OBJ_window : TrajectoryAffectable
{
    public Vector2 size;
    public GameObject body;
	public GameObject contents;

    SpriteRenderer sprite;
	[SerializeField] Sprite[] sprites;
	bool hasIcon = false;

	bool jumpOnNext = false;
	public bool presentAtStart = false;

    public override IEnumerable<Trajectory> Trajectories()
    {
		return new List<Trajectory>(){
			new Trajectory(transform, new Vector2(size.x, 0), GAME.plyrMvt.JumpLength() * 1.5f),
            new Trajectory(transform, new Vector2(size.x + GAME.plyrMvt.JumpLength()/2, GAME.plyrMvt.jumpHeight), GAME.plyrMvt.JumpLength() * 1.5f)
        };
    }

	public override void SetBounds()
	{
        bounds = body.GetComponent<Collider2D>().bounds;
	}

    public override void Start()
    {

		length = bounds.size.x;

		sprite = body.GetComponent<SpriteRenderer>();

		float aspectRatio = size.y / size.x;
		hasIcon = aspectRatio > 0.9f && aspectRatio < 1.2f;
		if (hasIcon)
		{
			contents.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
		}
		contents.transform.localPosition = size / 2 * new Vector2(1, -1);
		contents.transform.localScale = Vector2.one * Mathf.Min(size.x, size.y) * .6f;
		var layer = Random.Range(-20000, 20000);
		sprite.sortingOrder = 2 * layer;
		contents.GetComponent<SpriteRenderer>().sortingOrder = 2 * layer + 1;

		sprite.size = size;
		body.GetComponent<BoxCollider2D>().size = size;
		body.transform.localPosition = size / 2 * new Vector2(1, -1);
        base.Start();
	}
}
