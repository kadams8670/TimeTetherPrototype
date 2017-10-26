using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShellScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter2D(Collision2D colliderHit)
	{
		if(colliderHit.gameObject.CompareTag("Guard"))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
