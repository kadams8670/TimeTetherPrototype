using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDoor : MonoBehaviour 
{
	SpriteRenderer rend; 
	BoxCollider2D col; 

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<SpriteRenderer>(); 
		col = GetComponent<BoxCollider2D>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Temp_LevelStateManager.inst.securityAlertActive)
		{
			//gameObject.SetActive(true); 
			rend.enabled = true; 
			col.enabled = true; 
		}
		else
		{
			//gameObject.SetActive(false); 
			rend.enabled = false; 
			col.enabled = false; 
		}
	}
}
