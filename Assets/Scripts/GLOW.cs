using UnityEngine;

public class GLOW : MonoBehaviour
{
    [SerializeField] float glow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.SetFloat("_glow", glow);
    }
}
