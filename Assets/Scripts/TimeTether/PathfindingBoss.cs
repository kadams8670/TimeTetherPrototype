using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathfindingBoss : MonoBehaviour 
{
	public int currentWayPoint = 0;
	public GameObject[] wayPoints;
	public float radius;
	public float moveSpeed;
	public float turnSpeed;
	public LayerMask playerLayer;

	// Use this for initialization
	void Start () 
	{
		transform.localScale = new Vector3 (radius * 2, radius * 2, 1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(currentWayPoint < wayPoints.Length)
			transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);
		if (Vector3.Distance (transform.position, wayPoints [currentWayPoint].transform.position) < 0.1f)
			currentWayPoint++;
		Debug.DrawLine (transform.position, transform.position + transform.up, Color.yellow);
		LookForPlayer ();
		Rotate ();
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, radius);
	}

	void LookForPlayer()
	{
		Collider2D col = Physics2D.OverlapCircle (transform.position, radius, playerLayer);
		if (col != null)
			KillPlayer ();
	}

	void KillPlayer()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	void Rotate()
	{
		if (currentWayPoint >= wayPoints.Length)
			return;
		else
		{
			//rotate to face waypoint
			Vector3 dir = wayPoints[currentWayPoint].transform.position - transform.position;
			float angle = Mathf.Atan2 (-dir.x, dir.y) * Mathf.Rad2Deg;
			Quaternion rot = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime * turnSpeed);
			//move to current waypoint

		}
	}
}
