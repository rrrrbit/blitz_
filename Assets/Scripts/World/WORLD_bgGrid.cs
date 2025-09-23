using UnityEngine;

public class WORLD_bgGrid : MonoBehaviour
{
    Material material;

    [SerializeField] float Size;
    [SerializeField] float Width;
    [SerializeField] float Offset;

    float size { get { return material.GetFloat("_size"); } set { material.SetFloat("_size", value); } }
    float width { get { return material.GetFloat("_width"); } set { material.SetFloat("_width", value); } }
    public Vector2 offset { get { return material.GetVector("_offset"); } set { material.SetVector("_offset", value); } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //offset -= GAME.mgr.baseSpeed * Time.deltaTime;
    }
}
