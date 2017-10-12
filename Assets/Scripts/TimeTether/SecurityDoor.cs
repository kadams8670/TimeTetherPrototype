using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDoor : MonoBehaviour 
{
	SpriteRenderer rend; 
	BoxCollider2D col; 

	public bool invertState; 

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<SpriteRenderer>(); 
		col = GetComponent<BoxCollider2D>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!invertState)
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
		else
		{
			if (Temp_LevelStateManager.inst.securityAlertActive)
			{
				rend.enabled = false; 
				col.enabled = false; 
			}
			else
			{ 
				rend.enabled = true; 
				col.enabled = true; 
			}
		}
	}
}
