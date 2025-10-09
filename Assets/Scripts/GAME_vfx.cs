using System.Collections;
using UnityEngine;

public class GAME_vfx : MonoBehaviour
{
    public Material fxGlitch;
    public Material fxGlitchGlobal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GAME.vfx.fxGlitchGlobal.SetVector("_strength", new(0, 0));
        if (GAME_globalData.instance.quickGameTransition)
        {
            StartCoroutine(StartGlitch());
        }
        else
        {
            fxGlitch.SetVector("_strength", new(0, 0));
        }
    }

    IEnumerator StartGlitch()
    {
        fxGlitch.SetVector("_strength", new(30, 0));
        yield return new WaitForSecondsRealtime(0.1f);
        fxGlitch.SetVector("_strength", new(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        fxGlitch.SetFloat("_seed", Time.unscaledTime);
        fxGlitchGlobal.SetFloat("_seed", Time.unscaledTime+1);

    }
}
