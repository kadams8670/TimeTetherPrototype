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
	[SerializeField]
	private float cooldownMax = 2f;
	private float cooldownCur = 0f;

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
	private float currJumpDistance;

	private GameObject jumpTarget;

	/* Instance Methods */
	public override void Awake ()
	{
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
		if (cooldownCur > 0f)
		{
			cooldownCur -= Time.deltaTime;
			return;
		}

		if (!canJump)
			return;

		//released jump key, perform the jump
		if (Input.GetKeyUp (use_ability))
		{
			Vector3 dir = transform.up;
			if (teleportType == JumpMethod.mouse)
				dir = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			
			RaycastHit2D pathCheck = Physics2D.Raycast (transform.position, dir, currJumpDistance, 1 << LayerMask.NameToLayer("Wall"));

			if (pathCheck.collider == null)
			{
				transform.position = new Vector3 (
					jumpTarget.transform.position.x,
					jumpTarget.transform.position.y,
					0f);

				cooldownCur = cooldownMax;
			}

			Destroy (jumpTarget);
			currJumpDistance = 0f;
		}

		//holding the jump key, increment distance
		if (jumpTarget != null && Input.GetKey (use_ability))
		{
			currJumpDistance += distanceIncrement * Time.deltaTime;
			if (currJumpDistance > maxJumpDistance)
				currJumpDistance = maxJumpDistance;

			updateJumpMarker ();
		}

		//pressed the jump key, init jump state
		if(Input.GetKeyDown(use_ability))
		{
			currJumpDistance = minJumpDistance;
			GameObject jtPref = Resources.Load<GameObject> ("JumpTarget");
			jumpTarget = Instantiate<GameObject> (jtPref, transform.position, Quaternion.identity);

			updateJumpMarker ();
		}
	}
	private void updateJumpMarker()
	{
		switch (teleportType)
		{
		case JumpMethod.direct:
			jumpTarget.transform.position = transform.position + transform.up * currJumpDistance;
			break;
		case JumpMethod.mouse:
			Vector3 mp = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f));
			float dist = Vector3.Distance (mp, transform.position);
			if (dist > currJumpDistance)
				jumpTarget.transform.position = transform.position + (mp - transform.position).normalized * currJumpDistance;
			else if(dist < minJumpDistance)
				jumpTarget.transform.position = transform.position + (mp - transform.position).normalized * minJumpDistance;
			else
				jumpTarget.transform.position = mp;
			break;
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

	public float cooldownPercentage()
	{
		return cooldownCur / cooldownMax;
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, minJumpDistance);
		Gizmos.DrawWireSphere (transform.position, maxJumpDistance);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, currJumpDistance);
	}
}
