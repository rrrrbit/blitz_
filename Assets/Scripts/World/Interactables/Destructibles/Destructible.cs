using UnityEngine;

public class Destructible : Interactable
{
    public int scoreVal = 1;
    public override void Slash(GameObject context)
    {
        base.Slash(context);
        GAME.mgr.AddScore(scoreVal);
        GAME.mgr.interactables.Remove(gameObject);
        Destroy(gameObject);
    }
}
