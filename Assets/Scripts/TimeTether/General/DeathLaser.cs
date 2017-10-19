﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLaser : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{ 
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		else if (col.CompareTag("Guard"))
		{ 
			col.gameObject.SetActive (false);
			gameObject.SetActive(false); 
		}
	}
}
