using UnityEngine;
using UnityEngine.UIElements;

public class BG_window : GAME_obj
{
    public Vector2 size;
    public GameObject body;
    public GameObject contents;

    SpriteRenderer sprite;

    [SerializeField] Sprite[] sprites;

    Transform contentsTransform;
    Sprite icon;

    bool hasIcon = false;

    private void UpdateSize()
    {
        sprite.size = size;
        body.transform.localPosition = size / 2 * new Vector2(1, -1);
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
        sprite = body.GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        UpdateSize();
        if (hasIcon)
        {
            contents.transform.localPosition = size / 2 * new Vector2(1, -1);
            contents.GetComponent<SpriteRenderer>().sprite = icon;
            contents.GetComponent<SpriteRenderer>().color = sprite.color;

            contents.transform.localScale = Vector2.one * Mathf.Min(size.x, size.y) * .6f;
        }
    }

    public override void Spawn(GAME_spawns.QueuedSpawn ctx)
    {
        size = new Vector2(Random.Range(10f, 50f), Random.Range(10f, 50f));
        SetContents();
    }
}
