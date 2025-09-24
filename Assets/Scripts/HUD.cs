using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;


    float visualSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        visualSpeed = GLOBAL.Lerpd(visualSpeed, GAME.mgr.speed, 0.5f, 0.1f, Time.deltaTime);
        
        speedText.text = "speed: " + Mathf.RoundToInt(visualSpeed * 3.6f) + "k/h";
    }
}
