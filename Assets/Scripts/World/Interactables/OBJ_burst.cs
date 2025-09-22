using UnityEngine;

public class OBJ_burst : Interactable
{
    [SerializeField] bool repeatable;
    public override void Slash(GameObject context)
    {
        PLAYER_baseMvt mvt = context.GetComponent<PLAYER_baseMvt>();

        if (mvt != null && (!beenSlashed || repeatable))
        {
            mvt.gameObject.GetComponent<Rigidbody2D>().linearVelocityX += mvt.boostForce * .5f;
            GAME.mgr.speedMult = 2f;
            GAME.mgr.baseSpeed += 1;
        }

        beenSlashed = true;
    }
}
