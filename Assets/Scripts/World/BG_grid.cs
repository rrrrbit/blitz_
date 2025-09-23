using UnityEngine;

public class BG_grid : MonoBehaviour
{
    Material material;

    [SerializeField] float Width;
    [SerializeField] Vector2 Offset;

    float width { get { return material.GetFloat("_width"); } set { material.SetFloat("_width", value); } }
    public Vector2 offset { get { return material.GetVector("_offset"); } set { material.SetVector("_offset", value); } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
        width = Width;
        offset = Offset;

    }

    // Update is called once per frame
    void Update()
    {
        offset += Vector2.left * GAME.mgr.speed * Time.deltaTime;
    }
}
