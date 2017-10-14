using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShifter : MonoBehaviour 
{
	public bool isInPast = false;
	public KeyCode timeShiftKey;
	public float channelTime = 2f;
	private float timer = 0;
	public GameObject chargeBar; 
	private float fullbarLength;

	// Use this for initialization
	void Start () 
	{
		fullbarLength = chargeBar.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		chargeBar.transform.position = transform.position + (Vector3.up * 0.5f);
		chargeBar.transform.localScale = new Vector3 (fullbarLength * (timer/channelTime), chargeBar.transform.localScale.y, chargeBar.transform.localScale.z);
		if (Input.GetKey (timeShiftKey)) 
		{
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			timer+=Time.deltaTime;
		}
		if (Input.GetKeyUp (timeShiftKey))
			timer = 0;
		if (timer >= channelTime) 
		{
			timer = 0;
			Shift ();
		}
	}

	public void Shift()
	{
		if (isInPast)
			transform.position = new Vector3(transform.position.x - 100, transform.position.y, transform.position.z);
		else
			transform.position = new Vector3(transform.position.x + 100, transform.position.y, transform.position.z);
		isInPast = !isInPast;
	}
}
