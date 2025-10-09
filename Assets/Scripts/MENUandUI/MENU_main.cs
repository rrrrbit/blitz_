using UnityEngine;
using UnityEngine.SceneManagement;
public class MENU_main : MonoBehaviour
{
    public Material fxGlitch, fxGlitchGlobal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fxGlitchGlobal.SetVector("_strength", new(0, 0));
        fxGlitch.SetVector("_strength", new(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        fxGlitch.SetFloat("_seed", Time.unscaledTime);
    }

    public void StartGame()
    {
        GAME_globalData.instance.quickGameTransition = false;
        SceneManager.LoadScene("game");
    }
}
