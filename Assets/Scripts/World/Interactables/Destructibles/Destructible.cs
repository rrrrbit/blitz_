using UnityEngine;

public class Destructible : GAME_obj, IInteractable
{
    public int scoreVal = 1;
    public bool beenSlashed { get; set; }
    public virtual void Slash(GameObject context)
    {
        GAME.mgr.AddScore(scoreVal);
        DestroyCleanup();
    }

    protected virtual void Start()
    {
        GAME.objMgr.interactables.Add(gameObject);
    }
}
