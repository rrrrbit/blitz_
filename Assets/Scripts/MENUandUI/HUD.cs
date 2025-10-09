using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image fadeIn;


    float visualSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeIn.enabled = !GAME_globalData.instance.quickGameTransition;
    }

    // Update is called once per frame
    void Update()
    {
        visualSpeed = GLOBAL.Lerpd(visualSpeed, GAME.mgr.speed, 0.5f, 0.1f, Time.deltaTime);
        
        speedText.text = ">> " + Mathf.RoundToInt(visualSpeed * 3.6f) + " K/H";
        scoreText.text = "** "+ GAME.mgr.score.ToString();
    }
}
