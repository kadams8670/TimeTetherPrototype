using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour 
{
	public Vector3 targetPos;
	private Vector3 startPos;
	public float moveSpeed;
	public bool xPositionReached = false;
	public bool yPositionReached = false;

	// Use this for initialization
	void Start () 
	{
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (xPositionReached && yPositionReached)
			Destroy (gameObject);
		
		if(startPos.x < targetPos.x)
		{
			//move right
			if (transform.position.x < targetPos.x)
				transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
			else if (!xPositionReached)
				xPositionReached = true;
		}
		else
		{
			//move left
			if (transform.position.x > targetPos.x)
				transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
			else if (!xPositionReached)
				xPositionReached = true;
		}
		if(startPos.y < targetPos.y)
		{
			//move up
			if (transform.position.y < targetPos.y)
				transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
			else if (!yPositionReached)
				yPositionReached = true;
		}
		else
		{
			//move down
			if (transform.position.y > targetPos.y)
				transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
			else if (!yPositionReached)
				yPositionReached = true;
		}
	}
}
