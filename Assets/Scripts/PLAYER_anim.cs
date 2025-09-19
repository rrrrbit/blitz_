using System;
using UnityEngine;

public class PLAYER_anim : MonoBehaviour
{
    public enum States
    {
        air, ground
    }

    public States state;
    States last;

    Material material;
    Rigidbody2D rb;

    public Vector2 visualVelOffs;

    float squashRatio { get { return material.GetFloat("_squashRatio"); } set { material.SetFloat("_squashRatio", value); } }
    Vector2 squashDir { get { return material.GetVector("_squashDir"); } set { material.SetVector("_squashDir", value); } }
    Vector2 squashOrigin { get { return material.GetVector("_squashOrigin"); } set { material.SetVector("_squashOrigin", value); } }

    float shearOrigin { get { return material.GetFloat("_shearOrigin"); } set { material.SetFloat("_shearOrigin", value); } }
    float shearAmt { get { return material.GetFloat("_shearAmt"); } set { material.SetFloat("_shearAmt", value); } }


    bool hitGround;
    
    [SerializeField] AnimationCurve squashOverVel;
    [SerializeField] AnimationCurve shearOverVelX;

    [SerializeField] SpriteRenderer face;
    [SerializeField] ParticleSystem groundParticle;

    [SerializeField] AnimationCurve faceOffsOverVelX;
    [SerializeField] AnimationCurve faceOffsOverVelY;

    Vector2 visualVel;

    // precalculated lerp factors
    float lfA, lfB, lfC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody2D>();

        lfA = GLOBAL.LerpdF(.5f, .05f);
        lfB = GLOBAL.LerpdF(.5f, .03f);
        lfC = GLOBAL.LerpdF(.3f, .05f);

    }

    void UpdateShader(float d)
    {


        Vector2 targetSquashDir = Vector2.zero;

        if (state == States.ground)
        {
            squashOrigin = Vector2.down * .5f;
            targetSquashDir = Vector2.right;

            if (last == States.air) 
            {
                squashDir = targetSquashDir;
                squashRatio = 1.3f;
            }
        }
        else
        {
            squashOrigin = Vector2.zero;
            targetSquashDir = visualVel;
        }

        float targetSquashRatio = 1f;

        if (state == States.air)
        {
            targetSquashRatio = squashOverVel.Evaluate(visualVel.magnitude);
        }

        squashDir = GLOBAL.Lerpd(squashDir, targetSquashDir, lfA, d);
        squashRatio = GLOBAL.Lerpd(squashRatio, targetSquashRatio, lfC, d);

        float targetShearOrigin = 0;
        float targetShearAmt = 0;
        if (state == States.ground)
        {
            targetShearOrigin = -.5f;
            targetShearAmt = shearOverVelX.Evaluate(Mathf.Abs(visualVel.x)) * Mathf.Sign(visualVel.y);
        }

        shearAmt =  GLOBAL.Lerpd(shearAmt, targetShearAmt, lfB, d);
        shearOrigin = GLOBAL.Lerpd(shearOrigin, targetShearOrigin, lfB, d);
    }

    void UpdateFace(float d)
    {
        Vector2 targetPos = new Vector2(
            faceOffsOverVelX.Evaluate(visualVel.x), 
            faceOffsOverVelY.Evaluate(visualVel.y)
            );

        if (state == States.ground && last == States.air) { face.transform.localPosition += Vector3.down * .075f; }
        else { face.transform.localPosition = GLOBAL.Lerpd(face.transform.localPosition, targetPos, lfC, d); }
        
    }

    // Update is called once per frame
    void Update()
    {
        float d = Time.deltaTime;
        visualVel = rb.linearVelocity + visualVelOffs;
        UpdateShader(d);
        UpdateFace(d);
        ParticleSystem.EmissionModule em = groundParticle.emission;
        em.enabled = state == States.ground;

        last = state;
    }
}
