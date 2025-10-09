using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_flickerWindow : MonoBehaviour, IFlicker
{
    [SerializeField] float startDelay;
    [SerializeField] float time;
    [SerializeField] float speed;
    float timer;

    [SerializeField] GameObject[] children;

    public void In()
    {
        timer = time + startDelay;
        foreach(var f in children)
        {
            f.GetComponent<IFlicker>().In();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = Mathf.Max(0, timer - Time.unscaledDeltaTime);
        var c = GetComponent<Image>().color;
        c.a = timer > time ? 0 : timer > 0 ? Mathf.Floor(timer * speed % 2) : 1;

        GetComponent<Image>().color = c;
    }
}
