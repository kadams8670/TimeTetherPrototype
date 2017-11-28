using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayLaserTrap : MonoBehaviour 
{
	public Vector3 endPoint;
	private Vector3 startPoint;
	public bool isTriggered = false;
	public float moveSpeed;

	public GameObject[] doors;
	public GuardAI[] guards;
	public GameObject laser;

	// Use this for initialization
	void Start () 
	{
		startPoint = transform.position;
		if (laser == null)
			laser = transform.Find ("LaserLine").gameObject;
		laser.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isTriggered)
		{
			//move downward
			if (transform.position.y > endPoint.y)
				transform.Translate (Vector3.down * moveSpeed * Time.deltaTime);
			else 
			{
				isTriggered = false;
				laser.SetActive (false);
				//open doors
				for(int i = 0; i < doors.Length; i++)
				{
					doors [i].SetActive (false);
				}
			}
		}
		else
		{
			//move back
			if (transform.position.y < startPoint.y)
				transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
			//Check if guards see player
			for(int i = 0; i < guards.Length; i++)
			{
				if(guards[i].hasTarget)
				{
					isTriggered = true;
					laser.SetActive (true);
					//lock doors
					for(int j = 0; j < doors.Length; j++)
					{
						doors [j].SetActive (true);
					}
				}
			}
		}
	}
}
