using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Slash()
    {

    }

    private void Start()
    {
        GAME_manager.manager.interactables.Add(gameObject);
    }
}
