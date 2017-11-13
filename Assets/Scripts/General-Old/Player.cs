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
	public float cooldownPerc { get { return cooldownCur / cooldownMax; } }
	private int charges;
	public int currCharges { get { return charges; } }
	[SerializeField]
	private int chargesMax = 3;
	public int maxCharges { get { return chargesMax; } }

	public bool canJump = true;

	public enum JumpMethod { direct, mouse }
	[SerializeField]
	private JumpMethod teleportType = JumpMethod.direct;
	[SerializeField]
	private float minJumpDistance = 5f;
	[SerializeField]
	private float maxJumpDistance = 10f;
	[SerializeField]
	private bool useIncrement = true;
	[SerializeField]
	private float distanceIncrement = 2f;
	[SerializeField]
	private float jumpSpeed = 10f;
	private float currJumpDistance;
	[HideInInspector]
	public GameObject jumpTarget;
	private Vector3 startJumpPosition;

	private BehaviorState prime;
	private BehaviorState jumping;

	/* Instance Methods */
	public override void Awake ()
	{
		base.Awake ();
		prime = new BehaviorState ("prime", this.updatePrime, this.fixedUpdatePrime, this.lateUpdatePrime);
		jumping = new BehaviorState("jumping", this.updateJumping, this.fuJumping, this.lateUpdatePrime);

		setState (prime);

		charges = chargesMax;

		direction = Vector2.zero;

		jumpTarget = null;

		//fix values to appropriate ranges
		if (minJumpDistance > maxJumpDistance)
			minJumpDistance = maxJumpDistance;

		distanceIncrement = Mathf.Abs (distanceIncrement);
	}

	public void Start()
	{
		
	}

	private void updatePrime()
	{
		cooldownCur -= Time.deltaTime;
		if (cooldownCur <= 0f)
		{
			if (charges < chargesMax)
			{
				charges++;
				if (charges != chargesMax)
					cooldownCur = 0f;
				cooldownCur = cooldownMax;
			}
		}

		if (charges < 1)
			return;

		if (!canJump)
			return;

		//released jump key, perform the jump
		if (Input.GetKeyUp (use_ability))
		{
			startJumpPosition = transform.position;

			setState (jumping);

			charges--;
			if (cooldownCur <= 0f)
				cooldownCur = cooldownMax;

			currJumpDistance = 0f;
			return;
		}

		//holding the jump key, increment distance
		if (jumpTarget != null && Input.GetKey (use_ability))
		{
			if (useIncrement)
			{
				currJumpDistance += distanceIncrement * Time.deltaTime;
				if (currJumpDistance > maxJumpDistance)
					currJumpDistance = maxJumpDistance;
			}

			updateJumpMarker ();
		}

		//pressed the jump key, init jump state
		if(Input.GetKeyDown(use_ability))
		{
			currJumpDistance = useIncrement ? minJumpDistance : maxJumpDistance;
			GameObject jtPref = Resources.Load<GameObject> ("JumpTarget");
			jumpTarget = Instantiate<GameObject> (jtPref, transform.position, Quaternion.identity);

			updateJumpMarker ();
		}
	}
	private void updateJumpMarker()
	{
		float jd = currJumpDistance;

		Vector3 dir = transform.up;
		if (teleportType == JumpMethod.mouse)
			dir = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;

		float colliderRadius = GetComponent<CircleCollider2D> ().radius;
		RaycastHit2D[] pathCheck = Physics2D.CircleCastAll (transform.position, colliderRadius, dir, currJumpDistance, 1 << LayerMask.NameToLayer ("Wall"));

		if (pathCheck != null)
		{
			RaycastHit2D nearest = default(RaycastHit2D);
			foreach (RaycastHit2D hit in pathCheck)
			{
				if (nearest == default(RaycastHit2D))
					nearest = hit;
				else if (hit.distance < nearest.distance)
					nearest = hit;
			}

			if (nearest != default(RaycastHit2D) && nearest.distance < jd)
				jd = nearest.distance;
		}
		switch (teleportType)
		{
		case JumpMethod.direct:
			jumpTarget.transform.position = transform.position + transform.up * jd;
			break;
		case JumpMethod.mouse:
			Vector3 mp = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f));
			float dist = Vector3.Distance (mp, transform.position);
			if (dist > jd)
				jumpTarget.transform.position = transform.position + (mp - transform.position).normalized * jd;
			else if(dist < minJumpDistance)
				jumpTarget.transform.position = transform.position + (mp - transform.position).normalized * Mathf.Min(jd, minJumpDistance);
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

	private void updateJumping()
	{
		try
		{
			transform.position = Vector2.Lerp (transform.position, jumpTarget.transform.position, jumpSpeed * Time.deltaTime);
			if (Vector2.Distance (transform.position, jumpTarget.transform.position) < 0.1f)
			{
				Destroy (jumpTarget);
				setState (prime);
			}
		}
		catch(MissingReferenceException mre)
		{
			Debug.LogError (mre.Message);
			setState (prime);
		}
	}

	private void fuJumping()
	{

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
