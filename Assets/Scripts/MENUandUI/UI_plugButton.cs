using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_plugButton : MonoBehaviour
{
	public void GoToLink()
	{
		Application.OpenURL("https://r-bit.carrd.co");
	}
}
