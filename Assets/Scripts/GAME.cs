using UnityEngine;
using UnityEngine.SceneManagement;
public class GAME : MonoBehaviour
{
	public static GAME_manager mgr { get; private set; }
	public static GAME_objManager objMgr { get; private set; }
	public static GAME_vfx vfx {  get; private set; }
	public static PLAYER_baseMvt plyrMvt { get; private set; }

	[SerializeField] PLAYER_baseMvt PlyrMvt;
    public static CAMERA cam { get; private set; }

    [SerializeField] CAMERA Cam;

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
		SceneManager.LoadScene("menu_main");
	}

	public void Reload()
	{
        SceneManager.LoadScene("game");

    }
}
