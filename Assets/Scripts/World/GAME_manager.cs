using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAME_manager : MonoBehaviour
{

    public int score = 0;
    public float baseSpeed = 40;
    public float speedMult = 0;
    public float speed = 1;

    public Canvas hudCanvas;
    public Canvas deathCanvas;

    void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        speedMult = 0;
        yield return new WaitForSeconds(0.3f);
        speedMult = 1;
    }

    void Update()
    {
        speedMult = GLOBAL.Lerpd(speedMult, 1, .5f, .5f, Time.deltaTime);
        speed = baseSpeed * speedMult;
		baseSpeed += 1f / 6 * Time.deltaTime;
    }

    public void AddScore(int amt)
    {
        score += amt;
    }

    public void End()
    {
        print("end");
        
        
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        Time.timeScale = 0;
        //hudCanvas.enabled = false;
        GAME.vfx.fxGlitch.SetVector("_strength", new(15, 0));
        yield return new WaitForSecondsRealtime(1f);
        deathCanvas.enabled = true;
        deathCanvas.transform.GetChild(0).GetComponent<IFlicker>().In();
        
    }
}
