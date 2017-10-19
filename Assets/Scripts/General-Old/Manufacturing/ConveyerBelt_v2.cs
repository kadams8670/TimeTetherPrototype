using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt_v2 : MonoBehaviour 
{
	public enum Directions {RIGHT, LEFT, DOWN, UP};

	[HideInInspector]
	public ConveyerController controller;


	public float acceleration = 1.0f;
	public float speedLimit = 5f;
	public Directions positiveDirection;
	public Directions reverseDirection;
	private Vector3 posDir;
	private Vector3 revDir;

	// Use this for initialization
	void Start () 
	{
		controller = transform.GetComponentInParent<ConveyerController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerStay2D(Collider2D colliderHit)
	{
		if (!controller.isOn)
			return;

		Rigidbody2D rb2d = colliderHit.gameObject.GetComponent<Rigidbody2D> ();

		if (rb2d == null)
			return;

		if(!controller.isReversed) 
		{
			switch(positiveDirection)
			{
			case Directions.UP:
				if (rb2d.velocity.y > speedLimit)
					return;
				posDir = Vector3.up;
				break;
			case Directions.RIGHT:
				if (rb2d.velocity.x > speedLimit)
					return;
				posDir = Vector3.right;
				break;
			case Directions.DOWN:
				if (rb2d.velocity.y <  -1 * speedLimit)
					return;
				posDir = Vector3.down;
				break;
			case Directions.LEFT:
				if (rb2d.velocity.x < -1 * speedLimit)
					return;
				posDir = Vector3.left;
				break;
			}

			rb2d.AddForce (posDir * 100 * acceleration * controller.speedMod * Time.deltaTime);
		}
		else
		{
			switch(reverseDirection)
			{
			case Directions.UP:
				if (rb2d.velocity.y > speedLimit)
					return;
				revDir = Vector3.up;
				break;
			case Directions.RIGHT:
				if (rb2d.velocity.x > speedLimit)
					return;
				revDir = Vector3.right;
				break;
			case Directions.DOWN:
				if (rb2d.velocity.y <  -1 * speedLimit)
					return;
				revDir = Vector3.down;
				break;
			case Directions.LEFT:
				if (rb2d.velocity.x < -1 * speedLimit)
					return;
				revDir = Vector3.left;
				break;
			}
			rb2d.AddForce (revDir * 100 * acceleration * controller.speedMod * Time.deltaTime);
		}
		//print ("Moving Shit...");
	}



}
