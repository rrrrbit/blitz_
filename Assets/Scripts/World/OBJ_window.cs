using UnityEngine;

public class OBJ_window : GAME_obj
{
    public Vector2 size;
    public GameObject child;
	public GameObject contents;

    SpriteRenderer sprite;
    BoxCollider2D col;

	[SerializeField] Sprite[] sprites;

	Transform contentsTransform;
	Sprite icon;

	bool hasIcon = false;

    private void UpdateSize()
    {
        sprite.size = size;
        col.size = size;
        child.transform.localPosition = size / 2 * new Vector2(1,-1);
		length = size.x;

    }

	void SetContents()
	{
        float aspectRatio = size.y / size.x;

        hasIcon = aspectRatio > 0.9f && aspectRatio < 1.2f;

        if (hasIcon)
        {
            icon = sprites[Random.Range(0, sprites.Length)];
        }
    }
    
    private void Start()
    {
        sprite = child.GetComponent<SpriteRenderer>();
        col = child.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        UpdateSize();
        if (hasIcon)
        {
            contents.transform.localPosition = size / 2 * new Vector2(1, -1);
            contents.GetComponent<SpriteRenderer>().sprite = icon;
            
            contents.transform.localScale = Vector2.one * Mathf.Min(size.x, size.y) * .6f;
        }		
    }

	public override void Spawn(GAME_spawns.QueuedSpawn ctx)
	{
		var mvt = GAME.spawns.mvt;
		bool shouldJump = Random.value < 0.5f;
		transform.position += Vector3.left * GAME.spawns.grace;

        if (shouldJump)
        {
            size = new Vector2(Random.Range(5, 30), Random.Range(5, 20)) + Vector2.right * GAME.spawns.grace;
            float randomOffset = mvt.JumpLength() * Random.Range(.5f, 1.5f);
            Vector3 offsV = new(randomOffset, mvt.Trajectory(0, randomOffset));
            GAME.spawns.QueueSpawn(new(transform, offsV + Vector3.right * size.x, new() {
                {GAME.spawns.window, 4 },
                {GAME.spawns.relay, 2 },
                {GAME.spawns.burst, 1 }
            }));
        }
        else
        {
            size = new Vector2(Random.Range(5, 30), Random.Range(5, 20)) + Vector2.right * GAME.spawns.grace;
            if (Random.value < 0.325f && GAME.spawns.spawnQueue.Count < 2)
            {
                GAME.spawns.QueueSpawn(new(transform, Vector3.right * mvt.JumpLength() + Vector3.right * size.x, new() {
                    {GAME.spawns.relay, 2 },
                    {GAME.spawns.burst, 1 }
                }));
                float randomOffset = mvt.JumpLength() * Random.Range(.75f, 1);
                Vector3 offsV = new(randomOffset, -Mathf.Pow(2 * randomOffset / mvt.JumpLength(), 2) * mvt.jumpHeight);
                GAME.spawns.QueueSpawn(new(transform, offsV + Vector3.right * size.x, new() {
                    {GAME.spawns.window, 1 }
                }));
            }
            else
            {
                float randomOffset = mvt.JumpLength() * Random.Range(.25f, .75f);
                Vector3 offsV = new(randomOffset, -Mathf.Pow(2 * randomOffset / mvt.JumpLength(), 2) * mvt.jumpHeight);
                GAME.spawns.QueueSpawn(new(transform, offsV + Vector3.right * size.x, new() {
                    {GAME.spawns.window, 4 },
                    {GAME.spawns.relay, 2 },
                    {GAME.spawns.burst, 1 }
                }));
            }
        }
		GAME.spawns.objs.Add(gameObject);
		SetContents();
    }

	public override void SpawnStart()
	{
		var mvt = GAME.spawns.mvt;

		var jumpLength = mvt.jumpTime * GAME.mgr.baseSpeed;

		bool shouldJump = Random.value < 0.5f;

        if (shouldJump)
        {
            float randomOffset = Random.Range(-jumpLength / 2, jumpLength / 2);
            Vector3 offsV = new(randomOffset, -4 * randomOffset / jumpLength * mvt.jumpHeight * (1 + randomOffset / jumpLength));
            GAME.spawns.QueueSpawn(new(transform, Vector3.right * jumpLength + offsV + Vector3.right * size.x, new() {
                {GAME.spawns.window, 4 },
                {GAME.spawns.relay, 2 },
                {GAME.spawns.burst, 1 }
            }));
        }
        else
        {
            if (Random.value < 0.325f && GAME.spawns.spawnQueue.Count < 2)
            {
                GAME.spawns.QueueSpawn(new(transform, Vector3.right * jumpLength + Vector3.right * size.x, new() {
                    {GAME.spawns.relay, 2 },
                    {GAME.spawns.burst, 1 }
                }));
                float randomOffset = Random.Range(jumpLength * 0.75f, jumpLength);
                Vector3 offsV = new(randomOffset, -Mathf.Pow(2 * randomOffset / jumpLength, 2) * mvt.jumpHeight);
                GAME.spawns.QueueSpawn(new(transform, offsV + Vector3.right * size.x, new() {
                    {GAME.spawns.window, 1 }
                }));
            }
            else
            {
                float randomOffset = Random.Range(jumpLength * 0.25f, jumpLength * 0.75f);
                Vector3 offsV = new(randomOffset, -Mathf.Pow(2 * randomOffset / jumpLength, 2) * mvt.jumpHeight);
                GAME.spawns.QueueSpawn(new(transform, offsV + Vector3.right * size.x, new() {
                    {GAME.spawns.window, 4 },
                    {GAME.spawns.relay, 2 },
                    {GAME.spawns.burst, 1 }
                }));
            }
        }
        SetContents();

    }
}
