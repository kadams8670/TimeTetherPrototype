using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour 
{
	public bool activated; 

	[HideInInspector] public List<GameObject> collidingObjects; 

	[Tooltip("List of strings for tag names that will activate the pressure pad")]
	public List<string> affectedTags; 

	public Color activatedColor; 
	public Color offColor;

	SpriteRenderer rend; 

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<SpriteRenderer>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activated)
		{
			rend.color = activatedColor;
		}
		else
		{
			rend.color = offColor; 
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
				SwitchInteract(); 
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
				SwitchInteract(); 
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
