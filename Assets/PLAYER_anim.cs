using UnityEngine;

public class PLAYER_anim : MonoBehaviour
{
    public enum States
    {
        air, ground
    }

    public States state;

    Material material;
    Rigidbody2D mvt;

    [SerializeField] AnimationCurve squashOverVel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
        mvt = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_squashRatio",squashOverVel.Evaluate((mvt.linearVelocity+new Vector2(50, 0)).magnitude));
        material.SetFloat("_squashAngle", Mathf.Atan2(mvt.linearVelocity.y, -mvt.linearVelocity.x - 50));

        if (state == States.ground)
        {
            material.SetVector("_squashOrigin", new Vector2(0, -0.5f));
        }
        else
        {
            material.SetVector("_squashOrigin", new Vector2(0, 0));

        }

    }
}
