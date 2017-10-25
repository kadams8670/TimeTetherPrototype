using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : Controller
{
	/* Instance Vars */

	// A list of the states
	private BehaviorState[] states;

	[Header("Movement")]
	[SerializeField]
	private KeyCode up = KeyCode.W;
	[SerializeField]
	private KeyCode left = KeyCode.A;
	[SerializeField]
	private KeyCode down = KeyCode.S;
	[SerializeField]
	private KeyCode right = KeyCode.D;

	public bool canMove = true;

	private Vector2 direction;

	[SerializeField]
	public Stat movespeed = new Stat(0, 0);

	[Header("Teleport Ability")]
	[SerializeField]
	private KeyCode use_ability;

	public bool canJump = false;

	public enum JumpMethod { direct, mouse }
	[SerializeField]
	private JumpMethod teleportType = JumpMethod.direct;
	[SerializeField]
	private float minJumpDistance = 5f;
	[SerializeField]
	private float maxJumpDistance = 10f;
	[SerializeField]
	private float distanceIncrement = 2f;
	private float currJumpDistance = minJumpDistance;

	private GameObject jumpTarget;

	/* Instance Methods */
	public override void Awake ()
	{
		canMove = canJump = true;
		base.Awake ();
		setState (new BehaviorState("prime", this.updatePrime, this.fixedUpdatePrime, this.lateUpdatePrime));

		direction = Vector2.zero;

		jumpTarget = null;
	}

	public void Start()
	{
		
	}

	private void updatePrime()
	{
		if (!canJump)
			return;

		//released jump key, perform the jump
		if (Input.GetKeyUp (use_ability))
		{

		}

		//holding the jump key, increment distance
		if (Input.GetKey (use_ability))
		{
			currJumpDistance += distanceIncrement * Time.deltaTime;
			if (currJumpDistance > maxJumpDistance)
				currJumpDistance = maxJumpDistance;


		}

		//pressed the jump key, init jump state
		if(Input.GetKeyDown(use_ability))
		{
			currJumpDistance = minJumpDistance;

			switch (teleportType)
			{
			case JumpMethod.direct:
				
				break;
			case JumpMethod.mouse:

				break;
			}
		}


	}

	private void fixedUpdatePrime()
	{
		//movement
		Vector2 movementVector = Vector2.zero;

		float horizontal = Input.GetKey (left) ? -1f : Input.GetKey (right) ? 1f : 0f;
		float vertical = Input.GetKey (down) ? -1f : Input.GetKey (up) ? 1f : 0f;

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
