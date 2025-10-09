using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_textBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string text;
    [SerializeField] string hoverText;
    
    void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = hoverText;
        print("hovered");
    }

    public void OnPointerExit(PointerEventData data)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
