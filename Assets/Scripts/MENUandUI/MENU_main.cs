using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MENU_main : MonoBehaviour
{
    public Material fxGlitch;
	public Image blackScreen;

	public TOKEN_ControlScheme tokenControlScheme;

	InputSystem_Actions actions;
    InputSystem_Actions.MenuActions menuActions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fxGlitch.SetVector("_strength", new(0, 0));
		actions = new();
		actions.bindingMask = InputBinding.MaskByGroup(tokenControlScheme.GetScheme(actions).bindingGroup);
		menuActions = actions.menu;
        menuActions.Enable();
		
    }

    // Update is called once per frame
    void Update()
    {
        fxGlitch.SetFloat("_seed", Time.unscaledTime);
        if (menuActions.play.WasPressedThisFrame())
        {
            StartGame();
        }
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
