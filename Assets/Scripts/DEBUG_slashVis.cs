using UnityEditor;
using UnityEngine;

public class DEBUG_slashVis : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.a -= Time.deltaTime * 2;
        GetComponent<SpriteRenderer>().color = c;
    }
}
