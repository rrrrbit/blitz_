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
            print("has icon");
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
            contents.transform.localPosition = size / 2 * new Vector2(1, -1) + Vector2.down * 13 / 16;
            contents.GetComponent<SpriteRenderer>().sprite = icon;
            
            contents.transform.localScale = Vector2.one * Mathf.Min(size.x, size.y) * .7f;
            print("has icon");
        }		
    }

	public override void Spawn()
	{
		var mvt = GAME.spawns.mvt;

		var jumpLength = mvt.jumpTime * GAME.mgr.speed;

		bool shouldJump = Random.value < 0.5f;

		transform.position = GAME.spawns.spawnPos + GAME.spawns.nextSpawnOffs + Vector2.left * GAME.spawns.grace;

		if (shouldJump)
		{
			
			var randomOffset = Random.Range(-jumpLength / 2, jumpLength / 2);
			var offsV = new Vector3(randomOffset, -4 * randomOffset / GAME.mgr.speed / mvt.jumpTime * mvt.jumpHeight * (1 + randomOffset / GAME.mgr.speed / mvt.jumpTime));
			GAME.spawns.nextSpawnOffs = Vector3.right * jumpLength + offsV;
			size = new Vector2(Random.Range(10f, 50f), Random.Range(10f, 50f));
		}
		else
		{
			var randomOffset = Random.Range(jumpLength * 0.25f, jumpLength * 0.75f);
			var offsV = new Vector3(randomOffset, -Mathf.Pow(2 * randomOffset / GAME.mgr.speed / mvt.jumpTime, 2) * mvt.jumpHeight);
			GAME.spawns.nextSpawnOffs = offsV;
			size = new Vector2(Random.Range(10f, 20f), Random.Range(10f, 20f));
		}

		GAME.spawns.queue.Insert(0, gameObject);


		SetContents();
        
    }

	public override void SpawnStart()
	{
		var mvt = GAME.spawns.mvt;

		var jumpLength = mvt.jumpTime * GAME.mgr.speed;

		bool shouldJump = Random.value < 0.75f;

		if (shouldJump)
		{
			var randomOffset = Random.Range(-jumpLength / 2, jumpLength / 2);
			var offsV = new Vector3(randomOffset, -4 * randomOffset / GAME.mgr.speed / mvt.jumpTime * mvt.jumpHeight * (1 + randomOffset / GAME.mgr.speed / mvt.jumpTime));
			GAME.spawns.nextSpawnOffs = Vector3.right * jumpLength + offsV;
		}
		else
		{
			var randomOffset = Random.Range(jumpLength * 0.25f, jumpLength * 0.75f);
			var offsV = new Vector3(randomOffset, -Mathf.Pow(2 * randomOffset / GAME.mgr.speed / mvt.jumpTime, 2) * mvt.jumpHeight);
			GAME.spawns.nextSpawnOffs = offsV;
		}

        GAME.spawns.queue.Insert(0, gameObject);
		SetContents();

    }
}
