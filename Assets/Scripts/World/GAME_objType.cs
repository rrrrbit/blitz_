using UnityEngine;

[CreateAssetMenu(fileName = "Game Object Type", menuName = "Game Object Type", order = 1)]
public class GAME_objType : ScriptableObject
{
	public enum Category
	{
		p, np, misc
	}
	
	public GameObject obj;
	public float frequency;
	public Category category;
}