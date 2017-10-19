using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTLOS : MonoBehaviour 
{
	public float lineOfSight = 90;
	public float sightRange = 3;
	public int numberOfChecks = 12;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		SightCone ();	
	}

	void SightCone()
	{
		float currentAngle = lineOfSight * 0.5f;
		while(currentAngle >= 0)
		{
			Debug.DrawRay (transform.position, Quaternion.Euler (0, 0, currentAngle) * (transform.up) * (sightRange), Color.red);
			Debug.DrawRay (transform.position, Quaternion.Euler (0, 0, -currentAngle) * (transform.up) * (sightRange), Color.red);
			currentAngle -= (lineOfSight / (float)numberOfChecks);
		}
		Debug.DrawRay (transform.position, (transform.up) * (sightRange), Color.red);
	}
}
