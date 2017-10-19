using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteraction : MonoBehaviour 
{
	public KeyCode interactKey; 
	SimpleSwitch curSwitch; 

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(interactKey) && CheckForSimpleSwitch())
		{
			curSwitch.ToggleSwitch(); 
		}
	}

	bool CheckForSimpleSwitch()
	{
		//RaycastHit2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.01f); 
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.01f); 


		// Note that this will return the first BatteryNode found. The radius is small, so there should only be one found
		foreach (Collider2D col in hits)
		{
			if (col.gameObject.GetComponent<SimpleSwitch>() != null)
			{
				curSwitch = col.gameObject.GetComponent<SimpleSwitch>(); 
				return true; 
			}
		}
		return false; 
	}
}
