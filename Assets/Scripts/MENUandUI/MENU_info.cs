using UnityEngine;
using UnityEngine.UI;

public class MENU_info : MonoBehaviour
{
	public bool showInfo;
	public float time;
	public float opacity;
	public GameObject menu;
	public Scrollbar scrollbar;

	public float timer;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	public void Enable()
	{
		scrollbar.value = 1;
		showInfo = true;
	}
	public void Disable()
	{
		showInfo = false;
	}

    // Update is called once per frame
    void Update()
    {
        menu.SetActive(timer > 0);

		timer = Mathf.Clamp01(timer + Time.unscaledDeltaTime * (showInfo ? 1 : -1) / time);

		menu.GetComponent <CanvasGroup>().alpha = timer * opacity;
	}
}
