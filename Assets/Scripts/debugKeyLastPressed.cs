using TMPro;
using UnityEngine;


public class debugKeyLastPressed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.KeyDown || Event.current.keyCode != 0)
        {
            GetComponent<TextMeshProUGUI>().text = "DEBUG key pressed: " + Event.current.keyCode.ToString();
        }
    }


}
