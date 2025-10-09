using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAME_manager : MonoBehaviour
{

    public int score = 0;
    public float baseSpeed = 40;
    public float speedMult = 0;
    public float speed = 1;

    public Canvas hudCanvas;
    public Canvas deathCanvas;
    public Image dim;
    void Start()
    {
        if (!GAME_globalData.instance.quickGameTransition) { StartCoroutine(Init()); }
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
        dim.enabled = true;
        //hudCanvas.enabled = false;
        GAME.vfx.fxGlitch.SetVector("_strength", new(3, 0));
        yield return new WaitForSecondsRealtime(0.75f);
        deathCanvas.transform.GetChild(0).GetComponent<Flicker>().In();
        deathCanvas.enabled = true;
        
    }
}
