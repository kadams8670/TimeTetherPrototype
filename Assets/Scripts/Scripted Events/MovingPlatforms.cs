using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour 
{
	public GameObject emptySpace;
	public GameObject[] spaces;

	public Transform endPoint;
	private Transform startPoint;

	public float platformMoveSpeed = 2;
	public float platformToSpaceDelay = 1f;
	public float spaceToPlatformDelay = 2f;
		

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (SpawnPlatform ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator SpawnPlatform()
	{
		yield return new WaitForSeconds (spaceToPlatformDelay);
		int rand = Random.Range (0, spaces.Length);
		GameObject tempGO = Instantiate (spaces[rand], transform.position, Quaternion.identity);
		tempGO.GetComponent<MovingPlatform> ().targetPos = endPoint.position;
		tempGO.GetComponent<MovingPlatform> ().moveSpeed = platformMoveSpeed;
		StartCoroutine (SpawnSpace ());
	}

	IEnumerator SpawnSpace()
	{
		yield return new WaitForSeconds (platformToSpaceDelay);
		GameObject tempGO = Instantiate (emptySpace, transform.position, Quaternion.identity);
		tempGO.GetComponent<MovingPlatform> ().targetPos = endPoint.position;
		tempGO.GetComponent<MovingPlatform> ().moveSpeed = platformMoveSpeed;
		StartCoroutine (SpawnPlatform ());
	}
}
