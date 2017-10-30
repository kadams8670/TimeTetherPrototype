using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisTrigger : MonoBehaviour 
{
	public bool toggleLockedState; 
	public SavableGameObject savableGameObject; 
	public SpriteRenderer statisActiveSprite; 
	public FrozenObject toggleFrozenObject; 

	public List<GameObject> collidingStasisFields; 

	// Could list booleans determining what triggers this script



	
	// Update is called once per frame
	void Update () 
	{			
		if (savableGameObject != null)
		{
			if (toggleLockedState)
			{
				toggleLockedState = false; 

				Toggle(!savableGameObject.GetLocked()); 
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

		// Check for the removal of all stasis fields
		if (CollidingStasisFieldsIsNull())
		{
			Debug.Log("Stasis field reset test"); 
			Toggle(false);
			collidingStasisFields.Clear(); 
		}


	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("StasisField"))
		{
			Toggle(true); 
			collidingStasisFields.Add(other.gameObject); 
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("StasisField"))
		{
			Toggle(false); 
			collidingStasisFields.Remove(other.gameObject); 
		}
	}

	void Toggle(bool newLockedState)
	{
		if (toggleFrozenObject != null)
		{
			toggleFrozenObject.frozen = newLockedState; 
		}

		savableGameObject.SetLocked(newLockedState); 
	}
		

	bool CollidingStasisFieldsIsNull()
	{
		if (collidingStasisFields.Count > 0)
		{
			// As long as one of the fields isn't null, keep counting the collision
			for (int i = 0; i < collidingStasisFields.Count; i++)
			{
				if (collidingStasisFields[i] != null)
				{
					return false; 
				}
			}
			return true; 
		}

		return false; 
	}
}
