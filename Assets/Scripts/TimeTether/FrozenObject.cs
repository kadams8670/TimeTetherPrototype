using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObject : MonoBehaviour 
{
	public bool frozen;
	bool lastValue; 

	[Tooltip("Set to true for objects in normal world, false for other world")]
	public bool invertFrozen; 

	//public Component[] affectedComponents; 
	public MonoBehaviour[] affectedScripts; 

	// Use this for initialization
	void Start () 
	{
		UpdateComponents(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (lastValue != frozen)
		{
			//Debug.Log("Test frozen"); 

			if (frozen)
			{
				UpdateComponents(); 
			}
			else
			{
				UpdateComponents();
			}
		}
	}

	void UpdateComponents()
	{
		foreach (MonoBehaviour c in affectedScripts)
		{
			if (invertFrozen)
			{
				c.enabled = !frozen; 
			}
			else
			{
				c.enabled = frozen; 
			}
		}
		lastValue = frozen; 
	}
}
