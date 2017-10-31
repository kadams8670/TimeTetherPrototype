using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Patrol : MonoBehaviour
{

	public Transform[] patrolPoints;
	public float Speed;
	Transform currentPatrolPoint;
	int currentPatrolIndex;
	GuardAI otherAI;

	public bool canLoop;
	public bool isWandering;
	public bool isChasing;
	public bool endPatrol;
	public bool isOscillating;
	public bool teleportMovement;
	public float wanderTime;
	public float timer;
	private float wanderPointTimer;
	private float wanderPointDelay = 1.5f;
	public float WanderRange = 5;
	//write the wait at point for x seconds while at a point 
	public float waitAtPoint;
	//write rotation of guard while stationary 
	public int rotateGuardAngle;
	public float rotateGuardSpeed;
	private float curTime;
	public float wapypointPauseDuration;
	public float timeToNextRotate;

	Vector3 wanderTarget;
	Vector3 velocity;

	void Start()
	{
		wanderTarget = Vector3.zero;
		otherAI = gameObject.GetComponent<GuardAI> ();
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints[currentPatrolIndex];
		StartCoroutine(RotateObject(rotateGuardAngle, Vector3.forward, rotateGuardSpeed));
	}

	// Update is called once per frame
	void Update()
	{
		if (isOscillating == true) 
			Speed = 0f;

		if (otherAI.hasTarget) {
			isChasing = true;
			isOscillating = false;
		}


 		if (isChasing && !otherAI.hasTarget) 
		{
			isWandering = true;
			isChasing = false;
		}
		if(isChasing)
		{
			//do nothing for now
		}
		else if(isWandering)
		{
			timer += Time.deltaTime;
			if(timer >= wanderTime)
			{
				timer = 0;
				isWandering = false;
			}
			Wander ();
		}
		else
		{
			wanderTarget = Vector3.zero;
			Patrol ();

			if (curTime > 0)
				curTime -= Time.deltaTime; //pause over the waypoint
			else
				curTime = 0;

		}

	}

	void Patrol()
	{
		if(curTime <= 0) 
		{
			if (teleportMovement)
				transform.position = currentPatrolPoint.position;
			else
				transform.Translate (Vector3.up * Time.deltaTime * Speed);
		}

		if (endPatrol == false) 
		{

			if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
			{

				//We have reached the patrol point - get the next one 
				//Check to see if we have any patrol points - if not go back to the beginning
				if (currentPatrolIndex < patrolPoints.Length)
				{
					currentPatrolIndex++;
					if (currentPatrolIndex == patrolPoints.Length && canLoop)
						currentPatrolIndex = 0;
					curTime = wapypointPauseDuration;
				}
				currentPatrolPoint = patrolPoints[currentPatrolIndex];
			}

		}


		if (endPatrol == true) 
		{
			if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
			{

				//We have reached the patrol point - get the next one 
				//Check to see if we have any patrol points - if not go back to the beginning
				if (currentPatrolIndex < patrolPoints.Length)
				{
					currentPatrolIndex++;
					if (currentPatrolIndex == patrolPoints.Length)
						currentPatrolIndex--;
					curTime = wapypointPauseDuration;
				}
				currentPatrolPoint = patrolPoints[currentPatrolIndex];

			}
		}

		//Turn to face the current patrol point 
		//Finding direction Vector that points to patrol point
		Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
		//Figure out the rotation in degrees of the next patrol point we need to face 
		float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 90f;
		//Made the rotation to face
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		//Apply rotation to transform
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 5f);
	}

	void Wander()
	{
		transform.Translate(Vector3.up * Time.deltaTime * Speed * 0.7f);
		Debug.DrawLine (transform.position, wanderTarget, Color.green);

		if (wanderPointTimer < wanderPointDelay)
		{
			wanderPointTimer += Time.deltaTime;
		}
		else
		{
			wanderPointTimer = 0;
			Vector3 tempTarget = new Vector3(transform.position.x + Random.Range(-WanderRange, WanderRange+1), transform.position.y + Random.Range(-WanderRange, WanderRange+1), 0);

			RaycastHit2D hit = Physics2D.Linecast (transform.position, tempTarget, otherAI.visionLayers);

			if (hit.collider == null)
				wanderTarget = tempTarget;
			else
				wanderTarget = hit.point;
		}

		//Turn to face the current patrol point 
		//Finding direction Vector that points to patrol point
		Vector3 patrolPointDir = wanderTarget - transform.position;
		//Figure out the rotation in degrees of the next patrol point we need to face 
		float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 90f;
		//Made the rotation to face
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		//Apply rotation to transform
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);
	}

	IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
	{
		// calculate rotation speed
		float rotationSpeed = angle / inTime;

		while (true)
		{
			// save starting rotation position
			Quaternion startRotation = transform.rotation;

			float deltaAngle = 0;

			// rotate until reaching angle
			while (deltaAngle < angle)
			{
				deltaAngle += rotationSpeed * Time.deltaTime;
				deltaAngle = Mathf.Min(deltaAngle, angle);

				transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

				yield return null;
			}

			// delay here
			yield return new WaitForSeconds(timeToNextRotate);
		}
	}

}
