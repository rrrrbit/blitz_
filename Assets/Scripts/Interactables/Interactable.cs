using UnityEngine;

public class Interactable : GAME_obj
{
    protected bool beenSlashed = false;
    public virtual void Slash(GameObject context)
    {

    }

    protected virtual void Start()
    {
        GAME_manager.manager.interactables.Add(gameObject);
    }
}
