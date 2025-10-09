using UnityEngine;
using UnityEngine.UI;

public class UI_fadeOut : MonoBehaviour
{
    [SerializeField] float length;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = length;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Mathf.Max(0, timer - Time.deltaTime);
        var c = GetComponent<Image>().color;
        c.a = timer / length;
        GetComponent<Image>().color = c;
    }
}
