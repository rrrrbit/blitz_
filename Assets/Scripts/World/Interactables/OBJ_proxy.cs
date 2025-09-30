using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OBJ_proxy : Interactable
{
    [SerializeField] bool repeatable;
    [SerializeField] Transform inner;
    [SerializeField] Vector2 rotateSpeed;
    public override void Slash(GameObject context)
    {
        PLAYER_baseMvt mvt = context.GetComponent<PLAYER_baseMvt>();

        if (mvt != null && (!beenSlashed || repeatable))
        {
            
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
        base.Start();
        transform.eulerAngles.Set(0, 0, Random.Range(0, 90));
        inner.eulerAngles.Set(0, 0, Random.Range(0, 90));
    }

    public override void Spawn(GAME_spawns.QueuedSpawn ctx)
    {
        GAME.spawns.objs.Add(gameObject);
    }
}
