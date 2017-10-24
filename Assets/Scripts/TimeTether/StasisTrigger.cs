using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisTrigger : MonoBehaviour 
{
	public bool toggleLockedState; 
	public SavableGameObject savableGameObject; 
	public SpriteRenderer statisActiveSprite; 

	// Could list booleans determining what triggers this script



	
	// Update is called once per frame
	void Update () 
	{			
		if (savableGameObject != null)
		{
			if (toggleLockedState)
			{
				toggleLockedState = false; 
				savableGameObject.SetLocked(!savableGameObject.GetLocked()); 
			}
		}

		if (statisActiveSprite != null)
		{
			if (savableGameObject.GetLocked())
			{
				statisActiveSprite.enabled = true; 
			}
			else
			{
				statisActiveSprite.enabled = false; 
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("StasisField"))
		{
			savableGameObject.SetLocked(true); 
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("StasisField"))
		{
			savableGameObject.SetLocked(false); 
		}
	}
}
