using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : Controller
{
	private int numberOfInputs;
	private KeyCode nextButtonNumber;

	/* Instance Vars */

	[SerializeField]
	private KeyCode use_ability;

	// A list of the states
	private BehaviorState[] states;

	private Vector2 direction;

	[SerializeField]
	public Stat movespeed = new Stat(0, 0);

	public bool canMove = true;

	// Clone variables
	[SerializeField]
	private float cloneTimer;
	private Queue<KeyCode> userInputs;

	/* Instance Methods */
	public override void Awake ()
	{
		canMove = true;
		base.Awake ();
		setState (new BehaviorState("prime", this.updatePrime, this.fixedUpdatePrime, this.lateUpdatePrime));

		direction = Vector2.zero;

		userInputs = new Queue<KeyCode>();
	}

	// Toggle the special view layers off
	public void Start()
	{
		
	}

	private void updatePrime()
	{
		
	}

	private void fixedUpdatePrime()
	{
		//movement
		Vector2 movementVector = Vector2.zero;

		float horizontal = Input.GetAxis("Horizontal" + 1);
		float vertical = Input.GetAxis("Vertical" + 1);

		movementVector = new Vector2 (horizontal, vertical);

		if(canMove)
			physbody.AddForce (movementVector * movespeed.value);
		else
			physbody.velocity = Vector2.zero;
		
		if (movementVector != Vector2.zero)
			direction = movementVector;
		facePoint (direction + (Vector2)transform.position);
	}

	private void lateUpdatePrime()
	{

	}
}
