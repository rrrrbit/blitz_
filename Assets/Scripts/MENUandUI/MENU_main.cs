using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MENU_main : MonoBehaviour
{
    public Material fxGlitch;
	public Image blackScreen;

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

    public void StartGame()
    {
        GAME_globalData.instance.quickGameTransition = false;
		StartCoroutine(PlaySequence());
    }

	public void Exit()
	{
		StartCoroutine(ExitSequence());
	}

	IEnumerator ExitSequence()
	{
		fxGlitch.SetVector("_strength", new(30, 0));
		yield return new WaitForSecondsRealtime(0.05f);
		blackScreen.enabled = true;
		Application.Quit();
	}

	IEnumerator PlaySequence()
	{
		fxGlitch.SetVector("_strength", new(30, 0));
		yield return new WaitForSecondsRealtime(0.05f);
		blackScreen.enabled = true;
		SceneManager.LoadScene("game");
	}
}
