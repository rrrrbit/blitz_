using UnityEngine;

public class OBJ_window : GAME_obj
{
    public Vector2 size;
    public GameObject child;

    SpriteRenderer sprite;
    BoxCollider2D col;

    private void UpdateSize()
    {
        sprite.size = size;
        col.size = size;
        child.transform.position = size / 2 * new Vector2(1,-1);

    }

    private void Start()
    {
        sprite = child.GetComponent<SpriteRenderer>();
        col = child.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        UpdateSize();
    }
}
