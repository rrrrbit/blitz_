using UnityEngine;

public class GAME : MonoBehaviour
{
	public static GAME_manager mgr { get; private set; }
	public static GAME_objManager objMgr { get; private set; }

	public static PLAYER_baseMvt plyrMvt { get; private set; }

	[SerializeField] PLAYER_baseMvt PlyrMvt;

	private void Awake()
	{
		mgr = GetComponent<GAME_manager>();
		objMgr = GetComponent<GAME_objManager>();
		plyrMvt = PlyrMvt;
	}
}
