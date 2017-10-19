using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestroyer : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D col) 
	{
		CheckCollision(col.gameObject); 
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		CheckCollision(col.gameObject); 
	}

	void CheckCollision(GameObject g)
	{
		if (g.GetComponent<SpawnDestroy>() != null)
		{
			Destroy(g); 
		}
	}
}
