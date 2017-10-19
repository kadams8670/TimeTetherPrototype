using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour 
{
	public bool isOpen = false;
	public bool lockedToPlayer;

	public ButtonScript linkedButton;
	public ItemDetector itemDetector;

	public float timeDelay; //if 0, stays open
	private float timer = 0;
	private Vector2 startLocation;
	public Vector2 openLocation;
	public GameObject player;
	public float distanceToPlayer;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		startLocation.x = transform.position.x;	
		startLocation.y = transform.position.y;	
		/*if (linkedButton == null && itemDetector == null && flashTrigger == null)
			timeDelay = 0;*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
		DoorControl ();
		if (isOpen && timeDelay != 0 && timer == 0)
			isOpen = false;
	}

	void DoorControl()
	{
		//countdown timer
		if(timeDelay > 0)
		{
			if (timer > 0)
				timer-= Time.deltaTime;
			else
				timer = 0;
		}

		if (isOpen) 
		{
			transform.position = new Vector3 (openLocation.x, openLocation.y, 0);
		} 
		else 
		{
			transform.position = new Vector3 (startLocation.x, startLocation.y, 0);
		}

		if(linkedButton != null)
		{
			//check button
			if (!isOpen && linkedButton.isTriggered) 
			{
				isOpen = true;
				timer = timeDelay;
			}

		}
		else if(itemDetector != null)
		{
			//check detector
			if (!isOpen && itemDetector.isActive) 
			{
				isOpen = true;
				timer = timeDelay;
			}
		}
		else
		{
			if(Vector3.Distance(player.transform.position, transform.position) <= 0.75f && Input.GetKeyDown(KeyCode.E) && !isOpen && !lockedToPlayer)
			{
				isOpen = true;
			}
		}
	}
	public void Open()
	{
		isOpen = true;
		timer = timeDelay;
	}

	public void Open(float time)
	{
		isOpen = true;
		timer = time;
	}


}
