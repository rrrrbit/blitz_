using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GAME : MonoBehaviour
{
	public static GAME_manager mgr { get; private set; }
	public static GAME_objManager objMgr { get; private set; }
	public static GAME_vfx vfx {  get; private set; }
	public static PLAYER_baseMvt plyrMvt { get; private set; }

	[SerializeField] PLAYER_baseMvt PlyrMvt;
    public static CAMERA cam { get; private set; }

    [SerializeField] CAMERA Cam;

	[SerializeField] Image blackScreen;

    private void Awake()
	{
		mgr = GetComponent<GAME_manager>();
		objMgr = GetComponent<GAME_objManager>();
        vfx = GetComponent<GAME_vfx>();

        plyrMvt = PlyrMvt;
		cam = Cam;
		Time.timeScale = 1;
	}

	public void SaveAndExit()
	{
        StartCoroutine(ExitSequence());
    }

    IEnumerator ExitSequence()
    {
        vfx.fxGlitchGlobal.SetVector("_strength", new(30, 0));
        yield return new WaitForSecondsRealtime(0.1f);
        blackScreen.enabled = true;
        SceneManager.LoadScene("menu_main");
    }

    public void Reload()
	{
        StartCoroutine(ReloadSequence());

    }

	IEnumerator ReloadSequence()
	{
		vfx.fxGlitchGlobal.SetVector("_strength", new(30, 0));
        yield return new WaitForSecondsRealtime(0.1f);
		blackScreen.enabled = true;
        GAME_globalData.instance.quickGameTransition = true;
        SceneManager.LoadScene("game");
    }
}
