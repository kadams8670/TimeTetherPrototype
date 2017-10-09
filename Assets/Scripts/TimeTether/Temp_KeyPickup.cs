using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_KeyPickup : MonoBehaviour 
{
	// Used to solve a glitch with the onTriggerEnter triggering twice
	bool collected = false; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		collected = false; 
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") && !collected)
		{
			collected = true; 
			Temp_LevelStateManager.inst.playerNumKeys++; 
			//Destroy(this.gameObject); 
			gameObject.SetActive(false); 
		}
	}

}
