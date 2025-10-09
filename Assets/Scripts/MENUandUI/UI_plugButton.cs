using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_plugButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		GetComponentInChildren<Image>().material.SetFloat("_glow", 1);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void GoToLink()
	{
		Application.OpenURL("https://r-bit.carrd.co");
	}

	public void OnPointerEnter(PointerEventData data)
	{
		GetComponentInChildren<Image>().material.SetFloat("_glow", 2);
		print("!");
	}

	public void OnPointerExit(PointerEventData data)
	{
		GetComponentInChildren<Image>().material.SetFloat("_glow", 1);
	}
}
