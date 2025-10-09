using TMPro;
using UnityEngine;

public class UI_flickerTextIn : MonoBehaviour
{
    [SerializeField] float startDelay;
    [SerializeField] float time;
    [SerializeField] float seed;
    [SerializeField] float speed;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        In();
    }

    void In()
    {
        timer = time + startDelay;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Mathf.Max(0, timer-Time.deltaTime);
        
        var c = GetComponent<TextMeshProUGUI>().color;
        c.a = timer > time ? 0 : timer > 0 ? Mathf.Floor(timer * speed % 2) : 1;

        GetComponent<TextMeshProUGUI> ().color = c;
    }
}
