using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_flickerImage : Flicker
{
    [SerializeField] GameObject[] children;
    protected override void Update()
    {
        base.Update();

        var c = GetComponent<Image>().color;
        c.a = alpha;
        GetComponent<Image>().color = c;
    }

    public override void In()
    {
        base.In();
        foreach (var f in children)
        {
            f.GetComponent<Flicker>().In();
        }
    }
}
