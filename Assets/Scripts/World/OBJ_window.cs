using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class OBJ_window : GAME_obj
{
    public Vector2 size;
    public GameObject body;
	public GameObject contents;

    SpriteRenderer sprite;
	[SerializeField] Sprite[] sprites;
	bool hasIcon = false;

	bool jumpOnNext = false;
	public bool presentAtStart = false;
    
    private void Start()
    {
		sprite = body.GetComponent<SpriteRenderer>();

		jumpOnNext = Random.value < 0.5f;

		if (!presentAtStart)
		{
			size = new Vector2(length, Random.Range(5f,30f)) + Vector2.right * GAME.spawns.grace;
			transform.position += Vector3.left * GAME.spawns.grace;
			GAME.spawns.objs.Add(gameObject);
		}

		var mvt = GAME.spawns.mvt;

		if (jumpOnNext)
		{
			float randomOffset = mvt.JumpLength() * Random.Range(.5f, 1.5f);
			Vector3 offsV = new(randomOffset, mvt.Trajectory(0, randomOffset));
			GAME.spawns.QueueSpawn(new(transform, offsV + Vector3.right * size.x, new() {
				{GAME.spawns.window, 4 },
				{GAME.spawns.relay, 2 },
				{GAME.spawns.burst, 1 }
			},
			Random.Range(5f, 30f)));
		}
		else
		{
			float randomOffset = mvt.JumpLength() * Random.Range(0.25f, 0.75f);
			Vector3 offsV = new(randomOffset, mvt.Trajectory(.5f, randomOffset));
			GAME.spawns.QueueSpawn(new(transform, offsV + Vector3.right * size.x, new() {
				{GAME.spawns.window, 4 },
				{GAME.spawns.relay, 2 },
				{GAME.spawns.burst, 1 }
			},
			Random.Range(5f, 30f)));
		}

		float aspectRatio = size.y / size.x;
		hasIcon = aspectRatio > 0.9f && aspectRatio < 1.2f;
		if (hasIcon)
		{
			contents.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
		}
		contents.transform.localPosition = size / 2 * new Vector2(1, -1) + Vector2.down * 16/27f;
		contents.transform.localScale = Vector2.one * Mathf.Min(size.x, size.y) * .6f;
		var layer = Random.Range(-20000, 20000);
		sprite.sortingOrder = 2 * layer;
		contents.GetComponent<SpriteRenderer>().sortingOrder = 2 * layer + 1;

		sprite.size = size;
		body.GetComponent<BoxCollider2D>().size = size;
		body.transform.localPosition = size / 2 * new Vector2(1, -1);
	}
}
