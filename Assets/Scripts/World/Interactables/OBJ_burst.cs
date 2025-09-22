using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OBJ_burst : Interactable
{
    [SerializeField] bool repeatable;
    [SerializeField] Transform inner;
    [SerializeField] Vector2 rotateSpeed;
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

    void Update()
    {
        transform.eulerAngles += Vector3.forward * rotateSpeed.x * Time.deltaTime;
        inner.eulerAngles += Vector3.forward * rotateSpeed.y * Time.deltaTime;


    }

    protected override void Start()
    {
        transform.eulerAngles.Set(0, 0, Random.Range(0, 90));
        inner.eulerAngles.Set(0, 0, Random.Range(0, 60));
    }
}
