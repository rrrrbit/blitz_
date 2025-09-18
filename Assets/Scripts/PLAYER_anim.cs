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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody2D>();
    }

    void UpdateShader()
    {


        Vector2 targetSquashDir = Vector2.zero;

        if (state == States.ground)
        {
            squashOrigin = Vector2.down * .5f;
            targetSquashDir = Vector2.right;

            if (last == States.air) 
            {
                squashDir = targetSquashDir;
                squashRatio = 1.5f;
            }
        }
        else
        {
            squashOrigin = Vector2.zero;
            targetSquashDir = visualVel;
        }

        float targetSquashRatio = 1f;

        if (!GetComponent<PLAYER_baseMvt>().grounded)
        {
            targetSquashRatio = squashOverVel.Evaluate(visualVel.magnitude);
        }

        squashDir = Lerpd(squashDir, targetSquashDir, .5f, .05f, Time.deltaTime);
        squashRatio = Lerpd(squashRatio, targetSquashRatio, .3f, .05f, Time.deltaTime);

        float targetShearOrigin = 0;
        float targetShearAmt = 0;
        if (state == States.ground)
        {
            targetShearOrigin = -.5f;
            targetShearAmt = shearOverVelX.Evaluate(Mathf.Abs(visualVel.x)) * Mathf.Sign(visualVel.y);
        }

        shearAmt = Lerpd(shearAmt, targetShearAmt, .5f, .03f, Time.deltaTime);
        shearOrigin = Lerpd(shearOrigin, targetShearOrigin, .5f, .03f, Time.deltaTime);
    }

    void UpdateFace()
    {
        Vector2 targetPos = new Vector2(
            faceOffsOverVelX.Evaluate(visualVel.x), 
            faceOffsOverVelY.Evaluate(visualVel.y)
            );

        if (state == States.ground && last == States.air) { face.transform.localPosition += Vector3.down * .1f; }
        else { face.transform.localPosition = Lerpd(face.transform.localPosition, targetPos, .5f, .05f, Time.deltaTime); }
        
    }

    // Update is called once per frame
    void Update()
    {
        visualVel = rb.linearVelocity + visualVelOffs;
        UpdateShader();
        UpdateFace();
        ParticleSystem.EmissionModule em = groundParticle.emission;
        em.enabled = state == States.ground;

        last = state;
    }
	
	float Lerpd(float a, float b, float k, float t, float d) 
	{ 
		return Mathf.Lerp(
			a, b, 
			1 - Mathf.Pow(
				1 - k, 
				d / t)); 
	}
    Vector2 Lerpd(Vector2 a, Vector2 b, float k, float t, float d) 
	{ 
		return Vector2.Lerp(
			a, b, 
			1 - Mathf.Pow(
				1 - k, 
				d / t));
	}
}
