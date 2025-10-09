using UnityEngine;

public class GAME_vfx : MonoBehaviour
{
    public Material fxGlitch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fxGlitch.SetVector("_strength", new(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        fxGlitch.SetFloat("_seed", Time.unscaledTime);

    }
}
