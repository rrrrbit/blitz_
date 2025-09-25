using System.Collections.Generic;
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

		var jumpLength = mvt.jumpTime * GAME.mgr.speed;

		bool shouldJump = Random.value < 0.5f;

		transform.position += Vector3.left * GAME.spawns.grace;

        float randomOffset = shouldJump ? 
            Random.Range(-jumpLength / 2, jumpLength / 2) : 
            Random.Range(jumpLength * 0.25f, jumpLength * 0.75f);

        Vector3 offsV = new(randomOffset, shouldJump ? 
            (-4 * randomOffset / jumpLength * mvt.jumpHeight * (1 + randomOffset / jumpLength)) : 
            (-Mathf.Pow(2 * randomOffset / jumpLength, 2) * mvt.jumpHeight)
            );

        size = (shouldJump ?
            new(Random.Range(10f, 50f), Random.Range(10f, 50f)) :
            new(Random.Range(10f, 20f), Random.Range(10f, 20f)))
            + Vector2.right * GAME.spawns.grace;

        GAME.spawns.QueueSpawn(new(transform, (shouldJump ? Vector3.right : Vector3.zero) * jumpLength + offsV + Vector3.right * size.x, new()
            {
                {GAME.spawns.window, 4 },
                {GAME.spawns.relay, 2 },
                {GAME.spawns.burst, 1 }
            }));

        /*if (Random.value < 0.9f)
        {
            randomOffset = shouldJump ?
            Random.Range(jumpLength * 0.25f, jumpLength * 0.75f) :
            Random.Range(-jumpLength / 2, jumpLength / 2);

            offsV = new(randomOffset, shouldJump ?
            (-Mathf.Pow(2 * randomOffset / jumpLength, 2) * mvt.jumpHeight) :
            (-4 * randomOffset / jumpLength * mvt.jumpHeight * (1 + randomOffset / jumpLength)));

            GAME.spawns.QueueSpawn(new(transform, (shouldJump ? Vector3.zero : Vector3.right) * jumpLength + offsV + Vector3.right * size.x, new()
            {
                {GAME.spawns.window, 4 },
                {GAME.spawns.relay, 2 },
                {GAME.spawns.burst, 1 }
            }));
        }*/

		GAME.spawns.objs.Insert(0, gameObject);


		SetContents();
        
    }

	public override void SpawnStart()
	{
		var mvt = GAME.spawns.mvt;

		var jumpLength = mvt.jumpTime * GAME.mgr.baseSpeed;

		bool shouldJump = Random.value < 0.75f;

		if (shouldJump)
		{
			var randomOffset = Random.Range(-jumpLength / 2, jumpLength / 2);
			var offsV = new Vector3(randomOffset, -4 * randomOffset / jumpLength * mvt.jumpHeight * (1 + randomOffset / jumpLength));

            GAME.spawns.QueueSpawn(new(transform, Vector3.right * jumpLength + offsV + Vector3.right * size.x, new()
            {
                {GAME.spawns.window, 4 },
                {GAME.spawns.relay, 2 },
                {GAME.spawns.burst, 1 }
            }));

			//GAME.spawns.nextSpawnOffs = ;
		}
		else
		{
			var randomOffset = Random.Range(jumpLength * 0.25f, jumpLength * 0.75f);
			var offsV = new Vector3(randomOffset, -Mathf.Pow(2 * randomOffset / jumpLength, 2) * mvt.jumpHeight);

            GAME.spawns.QueueSpawn(new(transform, offsV + Vector3.right * size.x, new()
            {
                {GAME.spawns.window, 4 },
                {GAME.spawns.relay, 2 },
                {GAME.spawns.burst, 1 }
            }));
        }

        //GAME.spawns.objs.Insert(0, gameObject);
		SetContents();

    }
}
