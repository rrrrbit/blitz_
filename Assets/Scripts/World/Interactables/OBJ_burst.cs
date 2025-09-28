using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OBJ_burst : Interactable
{
    [SerializeField] bool repeatable;
    [SerializeField] Transform inner;
    [SerializeField] Vector2 rotateSpeed;
    [SerializeField] float boostTime;
    public override void Slash(GameObject context)
    {
        PLAYER_baseMvt mvt = context.GetComponent<PLAYER_baseMvt>();

        if (mvt != null && (!beenSlashed || repeatable))
        {
            StartCoroutine(Burst(mvt));
        }

        beenSlashed = true;
    }

    IEnumerator Burst(PLAYER_baseMvt mvt)
    {
        //mvt.gameObject.GetComponent<Rigidbody2D>().linearVelocityX += mvt.boostForce * .5f;
        GAME.mgr.speedMult += 1f;
        mvt.gravityMult = 0f;
        mvt.GetComponent<Rigidbody2D>().linearVelocityY = 0f;
        yield return new WaitForSeconds(boostTime - 0.05f);
        mvt.gravityMult = 1f;
        GAME.mgr.baseSpeed += 1;
        GAME.mgr.speedMult -= 1f;

    }

    void Update()
    {
        transform.eulerAngles += Vector3.forward * rotateSpeed.x * Time.deltaTime;
        inner.eulerAngles += Vector3.forward * rotateSpeed.y * Time.deltaTime;


    }

    protected override void Start()
    {
        base.Start();
        transform.eulerAngles.Set(0, 0, Random.Range(0, 90));
        inner.eulerAngles.Set(0, 0, Random.Range(0, 60));
    }

    public override void Spawn(GAME_spawns.QueuedSpawn ctx)
    {
        var mvt = GAME.spawns.mvt;
        var randomOffset = mvt.JumpLength() * Random.Range(.25f, .5f);
        Vector2 offsV = new(randomOffset, mvt.Trajectory(.5f, randomOffset));

        GAME.spawns.QueueSpawn(new(transform, offsV + Vector2.right * GAME.mgr.speed * boostTime, new()
        {
            {GAME.spawns.window, 8 },
            {GAME.spawns.relay, 1 },
            {GAME.spawns.burst, 1 }
        },
		Random.Range(5f, 30f)));

        GAME.spawns.objs.Add(gameObject);
    }
}
