using UnityEngine;
using UnityEngine.UIElements;

public class BG_window : GAME_obj
{
    public Vector2 size;
    public GameObject body;
    public GameObject contents;

    SpriteRenderer sprite;

    [SerializeField] Sprite[] contentSprites;
    [SerializeField] Sprite[] bodySprites;

    Transform contentsTransform;
    Sprite icon;

    bool hasIcon = false;

    private void UpdateSize()
    {
        contents.transform.localPosition = size / 2 * new Vector2(1, -1);
        contents.GetComponent<SpriteRenderer>().sprite = icon;
        contents.GetComponent<SpriteRenderer>().color = sprite.color;

        contents.transform.localScale = Vector2.one * Mathf.Min(size.x, size.y) * .6f;
        sprite.size = size;
        body.transform.localPosition = size / 2 * new Vector2(1, -1);
        float aspectRatio = size.y / size.x;

        hasIcon = aspectRatio > 0.9f && aspectRatio < 1.2f;

        if (hasIcon)
        {
            icon = contentSprites[Random.Range(0, contentSprites.Length)];
        }


    }

    private void Start()
    {
        sprite = body.GetComponent<SpriteRenderer>();
        sprite.sprite = bodySprites[Random.Range(0, bodySprites.Length)];
        size = new Vector2(Random.Range(10f, 50f), Random.Range(10f, 50f));
        var layer = Random.Range(-20000, 20000);
        sprite.sortingOrder = 2 * layer;
        contents.GetComponent<SpriteRenderer>().sortingOrder = 2 * layer + 1;
        UpdateSize();
    }

    protected override void Update()
    {
        base.Update();
    }
}
