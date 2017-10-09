using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			Debug.Log("Hit player"); 

			if (Temp_LevelStateManager.inst.playerNumKeys > 0)
			{
				Temp_LevelStateManager.inst.playerNumKeys--; 
				gameObject.SetActive(false); 
			}
		}
	}
	*/ 

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			if (Temp_LevelStateManager.inst.playerNumKeys > 0)
			{
				Temp_LevelStateManager.inst.playerNumKeys--; 
				gameObject.SetActive(false); 
			}
		}
	}
}
