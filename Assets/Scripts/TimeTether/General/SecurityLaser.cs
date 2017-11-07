﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityLaser : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void LateUpdate()
	{
		if (Temp_LevelStateManager.inst.securityAlertActive)
		{
			gameObject.SetActive(false); 
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{ 
			Temp_LevelStateManager.inst.securityAlertActive = true; 
			gameObject.SetActive(false); 
		}
	}
}
