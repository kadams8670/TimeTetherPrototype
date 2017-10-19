using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour 
{
	GameObject destination; 

	// Use this for initialization
	void Start () 
	{
		destination = transform.Find("Dest").gameObject; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{ 
			col.transform.position = destination.transform.position; 
		}
	}
}
