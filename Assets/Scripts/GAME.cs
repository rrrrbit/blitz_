using UnityEngine;

public class GAME : MonoBehaviour
{
	public static GAME_manager mgr { get; private set; }
	public static GAME_spawns spawns { get; private set; }

	private void Awake()
	{
		mgr = GetComponent<GAME_manager>();
		spawns = GetComponent<GAME_spawns>();
	}
}
