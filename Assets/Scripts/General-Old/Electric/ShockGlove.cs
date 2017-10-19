using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockGlove : MonoBehaviour 
{
	public int maxCharge; 
	public int curCharge; 

	Rigidbody2D rb; 

	public LayerMask passObstacles;

	public float maxPassDistance; 

	public GameObject otherPlayer;
	ShockGlove otherPlayerShockGlove; 

	public GameObject[] chargeUISprites; 
	public GameObject chargeUIParent; 

	// Key for transferring or passing electricity
	// This is currently designed as a single button, but it might eventually require 2 or 3 depending on needed actions
	public KeyCode useKey; 

	// If the player is over a battery, this will hold that reference
	BatteryNode curBattery; 

	// Used to avoid constant state toggling when button held
	bool justUsed;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>(); 
		otherPlayerShockGlove = otherPlayer.GetComponent<ShockGlove>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		 * Electricity simple UI display
		 */

		// UI for charge sprite- keep it at the same rotation
		if (chargeUIParent != null)
		{
			chargeUIParent.transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 0)); 
		}
		// Choose to display UI for each individual sprite
		for (int i = 0; i < maxCharge; i++)
		{
			if (chargeUISprites[i].Equals(null))
				continue; 

			if (curCharge >= i + 1)
			{
				chargeUISprites[i].SetActive(true); 
			}
			else
			{
				chargeUISprites[i].SetActive(false); 
			}
		}

		// Check for transferring to/from battery
		if (!justUsed && CheckForBattery())
		{
			BatteryInteract(); 
		}
		// Check for transferring to other player
		else if (CheckCanTransfer())
		{
			// Check interaction (instant key press to transfer)
			if (Input.GetKeyDown(useKey))
			{
				ShockTransfer(); 


			}
		}

		if (!Input.GetKey(useKey))
		{
			justUsed = false; 
		}
	}

	void ShockTransfer()
	{
		// Temporary transfer
		// Later, this could be given some more flair like a sprite that flies between the two players

		// Make sure player stats work and the distance does not exceed max distance
		if (curCharge > 0 && otherPlayerShockGlove.curCharge < otherPlayerShockGlove.maxCharge && Vector2.Distance(transform.position, otherPlayer.transform.position) < maxPassDistance)
		{
			curCharge--; 
			otherPlayerShockGlove.curCharge++;
		}

		 
	}

	void BatteryInteract()
	{
		// Test possible error
		if (curBattery == null)
		{
			Debug.LogError("curBattery is null"); 
			return; 
		}

		if (Input.GetKey(useKey))
		{
			// Temporary: (Mostly) stop the rigidbody from moving while interacting with batteries
			rb.velocity = new Vector2(0, 0); 

			// Determine whether to add or subtract charge from battery
			// Drain power
			if (curBattery.activated && curCharge < maxCharge)
			{
				// Drain the battery; Add to current charge if the action is completed and returns true
				if (curBattery.Interact(false))
				{
					curCharge++; 
					justUsed = true; 
				}
			}
			// Add power
			else if (!curBattery.activated && curCharge > 0)
			{
				// Add to the battery; Subtract from the current charge if the action is completed and returns true
				if (curBattery.Interact(true))
				{
					curCharge--; 
					justUsed = true; 
				}
			}
		}
		// If the user stopped pressing interact while draining, tell the battery to reset its interaction state
		else
		{
			curBattery.ResetInteract(); 
		}
	}


	bool CheckForBattery()
	{
		//RaycastHit2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.01f); 
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.01f); 


		// Note that this will return the first BatteryNode found. The radius is small, so there should only be one found
		foreach (Collider2D col in hits)
		{
			if (col.gameObject.GetComponent<BatteryNode>() != null)
			{
				curBattery = col.gameObject.GetComponent<BatteryNode>(); 
				return true; 
			}
		}
		return false; 
	}

	bool CheckCanTransfer()
	{
		if (Physics2D.Linecast(transform.position, otherPlayer.transform.position, passObstacles.value))
		{
			return false; 
		}

		return true; 
	}
}
