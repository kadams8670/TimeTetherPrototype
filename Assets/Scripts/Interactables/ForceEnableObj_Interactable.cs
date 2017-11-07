using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceEnableObj_Interactable : Interactable 
{
	public bool setEnable; 

	[Tooltip("Drag in the GameObject that will be affected")]
	public GameObject obj; 

	public override void OnInteract()
	{
		obj.SetActive(setEnable); 
	}
}
