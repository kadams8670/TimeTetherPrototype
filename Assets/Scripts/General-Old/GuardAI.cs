using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : MonoBehaviour 
{
	public LayerMask visionLayers;
	public float moveSpeed;

	public float lineOfSight = 90;
	public float sightRange = 3;
	public int numberOfChecks = 12;

	[HideInInspector]
	public Vector3 target;

	[HideInInspector]
	public bool hasTarget = false;

	private Transform visionCone;

	// Use this for initialization
	void Start () 
	{
		visionCone = transform.Find ("line of sight");
	}
	
	// Update is called once per frame
	void Update () 
	{	
		VisionCheck();
		if (Mathf.Abs (Vector3.Distance (transform.position, target)) < 0.25f && hasTarget)
			hasTarget = false;
		Movement ();
		ResizeLineOfSight ();
	}

	void Movement()
	{
		if(hasTarget)
		{
			Vector3 diff = transform.position - target;
			diff.Normalize();

			float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotation + 90);

			transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
		}
	}

	void ResizeLineOfSight()
	{
		//0.6697227 base, when length is 5
		visionCone.transform.localScale = new Vector3(0.6697227f * sightRange/5, 0.6697227f * sightRange/5, 1);
	}

	void VisionCheck()
	{
		float currentAngle = lineOfSight * 0.5f;
		while(currentAngle >= 0)
		{
			//check left side
			RaycastHit2D hit1 = (Physics2D.Raycast (transform.position, Quaternion.Euler (0, 0, currentAngle) * (transform.up), sightRange, visionLayers));
			if (hit1.collider != null) 
			{
				Debug.DrawLine (transform.position, hit1.point, Color.red);
				if(hit1.collider.CompareTag("Player"))
				{
					target = hit1.collider.transform.position;
					hasTarget = true;
				}
			}
			else
				Debug.DrawRay (transform.position, Quaternion.Euler (0, 0, currentAngle) * (transform.up) * (sightRange), Color.green);

			//check right side
			RaycastHit2D hit2 = (Physics2D.Raycast (transform.position, Quaternion.Euler (0, 0, -currentAngle) * (transform.up), sightRange, visionLayers));
			if (hit2.collider != null) 
			{
				Debug.DrawLine (transform.position, hit2.point, Color.red);
				if(hit2.collider.CompareTag("Player"))
				{
					target = hit2.collider.transform.position;
					hasTarget = true;
				}
			}
			else
				Debug.DrawRay (transform.position, Quaternion.Euler (0, 0, -currentAngle) * (transform.up) * (sightRange), Color.green);
			currentAngle -= (lineOfSight / (float)numberOfChecks);
		}

		//check directly forward
		RaycastHit2D hit3 = (Physics2D.Raycast (transform.position, transform.up, sightRange, visionLayers));
		if (hit3.collider != null) 
		{
			Debug.DrawLine (transform.position, hit3.point, Color.red);
			if(hit3.collider.CompareTag("Player"))
			{
				target = hit3.collider.transform.position;
				hasTarget = true;
			}
		}
		else
			Debug.DrawRay (transform.position, transform.up * sightRange, Color.green);
		
	}
}
