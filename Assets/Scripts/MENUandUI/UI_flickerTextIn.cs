using TMPro;
using UnityEngine;

public class UI_flickerTextIn : Flicker
{
    protected override void Update()
    {
        base.Update();

        var c = GetComponent<TextMeshProUGUI>().color;
        c.a = alpha;
        GetComponent<TextMeshProUGUI>().color = c;
    }
}
