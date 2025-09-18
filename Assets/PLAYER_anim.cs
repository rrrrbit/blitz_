using System;
using UnityEngine;

public class PLAYER_anim : MonoBehaviour
{
    public enum States
    {
        air, ground
    }

    public States state;

    Material material;
    Rigidbody2D rb;

    float squashRatio { get { return material.GetFloat("_squashRatio"); } set { material.SetFloat("_squashRatio", value); } }
    Vector2 squashDir { get { return material.GetVector("_squashDir"); } set { material.SetVector("_squashDir", value); } }
    Vector2 squashOrigin { get { return material.GetVector("_squashOrigin"); } set { material.SetVector("_squashOrigin", value); } }

    float shearOrigin { get { return material.GetFloat("_shearOrigin"); } set { material.SetFloat("_shearOrigin", value); } }
    float shearAmt { get { return material.GetFloat("_shearAmt"); } set { material.SetFloat("_shearAmt", value); } }


    bool snapSquashDirThisFrame;
    
    [SerializeField] AnimationCurve squashOverVel;
    [SerializeField] AnimationCurve shearOverVelX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 absVel = rb.linearVelocity + Vector2.right * 50;


        Vector2 targetSquashDir = Vector2.zero;

        if (state == States.ground)
        {
            squashOrigin = Vector2.down * .5f;
            targetSquashDir = Vector2.right;
            snapSquashDirThisFrame = true;
        }
        else
        {
            squashOrigin = Vector2.zero;
            targetSquashDir = rb.linearVelocity;
        }

        float targetSquashRatio = 1f;

        if (!GetComponent<PLAYER_baseMvt>().grounded)
        {
            targetSquashRatio = squashOverVel.Evaluate(rb.linearVelocity.magnitude);
        }

        if (snapSquashDirThisFrame)
        {
            squashDir = targetSquashDir;
            snapSquashDirThisFrame = false;
        }
        else { squashDir = Lerpd(squashDir, targetSquashDir, .5f, .05f, Time.deltaTime); }

        squashRatio = Lerpd(squashRatio, targetSquashRatio, .3f, .05f, Time.deltaTime);

        float targetShearOrigin = 0;
        float targetShearAmt = 0;
        if (state == States.ground)
        {
            targetShearOrigin = -.5f;
            targetShearAmt = shearOverVelX.Evaluate(Mathf.Abs(rb.linearVelocityX)) * Mathf.Sign(rb.linearVelocityX);
        }

        shearAmt = Lerpd(shearAmt, targetShearAmt, .5f, .03f, Time.deltaTime);
        shearOrigin = Lerpd(shearOrigin, targetShearOrigin, .5f, .03f, Time.deltaTime);

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
