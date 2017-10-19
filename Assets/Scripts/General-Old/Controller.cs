using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{
	/* Static Vars */


	/* Instance Vars */
	protected Animator anim;
	protected Rigidbody2D physbody;

	// The currently active state of this controller script
	private BehaviorState activeState;

	/* Static Methods */


	/* Instance Methods */
	public virtual void Awake()
	{
		anim = GetComponent<Animator> ();
		physbody = GetComponent<Rigidbody2D> ();
	}

	public void Update()
	{
		// Call currently active behavior
		if(activeState.update != null)
			activeState.update ();
	}

	public void FixedUpdate()
	{
		// Call currently active behavior
		if(activeState.fixedUpdate != null)
			activeState.fixedUpdate ();
	}

	public void LateUpdate()
	{
		// Call currently active behavior
		if(activeState.lateUpdate != null)
			activeState.lateUpdate ();
	}

	protected void setState(BehaviorState state)
	{
		activeState = state;
	}

	protected void facePoint(Vector2 point)
	{
		Quaternion rot = Quaternion.LookRotation (transform.position - new Vector3(point.x, point.y, -100f), Vector3.forward);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0f, 0f, transform.eulerAngles.z);
	}

	protected void faceTarget(Transform target)
	{
		if (transform != null)
			facePoint (transform.position);
	}

	protected void faceTargetLeading(Transform target, float bulletSpeed)
	{
		Rigidbody2D body = target.GetComponent<Rigidbody2D> ();
		if (body == null)
			throw new ArgumentException ("Tried to lead a velocity-less target.");

		float stepsToCollision = Vector2.Distance (transform.position, target.position);
		facePoint ((Vector2)target.position + (body.velocity * stepsToCollision));
	}

	/* Delegates */

	// Used for Update, FixedUpdate, and LateUpdate
	protected delegate void UpdateBehavior();

	/* Private Inner Class */
	protected struct BehaviorState
	{
		public string name;

		public UpdateBehavior update;
		public UpdateBehavior fixedUpdate;
		public UpdateBehavior lateUpdate;

		public BehaviorState(string name, UpdateBehavior update, UpdateBehavior fixedUpdate, UpdateBehavior lateUpdate)
		{
			this.name = name;

			this.update = update;
			this.fixedUpdate = fixedUpdate;
			this.lateUpdate = lateUpdate;
		}

		public override bool Equals (object obj)
		{
			return ((BehaviorState)obj).name == name;
		}
		public override int GetHashCode ()
		{
			return name.GetHashCode ();
		}
	}
}
