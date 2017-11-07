using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ForceEnableObj_Interactable))]
public class PressurePad : MonoBehaviour 
{
	public bool activated; 
	public bool inverted; 

	[HideInInspector] public List<GameObject> collidingObjects; 

	[Tooltip("List of strings for tag names that will activate the pressure pad")]
	public List<string> affectedTags; 

	public Color activatedColor; 
	public Color offColor;

	ForceEnableObj_Interactable forceEnableScript; 

	SpriteRenderer rend; 

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<SpriteRenderer>(); 
		forceEnableScript = GetComponent<ForceEnableObj_Interactable>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activated)
		{
			rend.color = activatedColor;

			forceEnableScript.setEnable = true;

			if (inverted)
				forceEnableScript.setEnable = false; 

			SwitchInteract(); 
		}
		else
		{
			rend.color = offColor; 

			forceEnableScript.setEnable = false; 

			if (inverted)
				forceEnableScript.setEnable = true; 

			SwitchInteract(); 
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (affectedTags.Contains(other.tag))
		{
			if (!collidingObjects.Contains(other.gameObject))
			{
				collidingObjects.Add(other.gameObject); 
			}

			if (!activated)
			{
				activated = true;

				/*
				forceEnableScript.setEnable = true;

				if (inverted)
					forceEnableScript.setEnable = false; 

				SwitchInteract(); 
				*/ 
			}
		} 
	}



	void OnTriggerExit2D(Collider2D other)
	{
		if (affectedTags.Contains(other.tag) && activated)
		{
			if (collidingObjects.Contains(other.gameObject))
			{
				collidingObjects.Remove(other.gameObject); 
			}

			if (collidingObjects.Count == 0)
			{
				activated = false; 

				/*
				forceEnableScript.setEnable = false; 

				if (inverted)
					forceEnableScript.setEnable = true; 
				
				SwitchInteract(); 
				*/ 
			}
		}
	}

	protected virtual void SwitchInteract()
	{
		foreach (Interactable i in GetComponents<Interactable>())
		{
			i.OnInteract(); 
		}
	}


}
