using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour 
{
	//255  0  0
	//113 72 72
	public bool isTriggered = false;
	public bool isReusable = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D colliderHit)
	{
		if (colliderHit.CompareTag ("Player") && !isTriggered)
		{
			isTriggered = true;
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.443f, 0.282f, 0.282f);
		}
	}

	void OnTriggerExit2D(Collider2D colliderHit)
	{
		if (colliderHit.CompareTag ("Player") && isReusable)
		{
			isTriggered = false;
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 0, 0);
		}
	}
}
