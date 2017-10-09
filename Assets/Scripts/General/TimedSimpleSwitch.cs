using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSimpleSwitch : SimpleSwitch 
{
	public float useTime; 
	float useTimer; 

	public GameObject progressBar; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		UpdateSwitchDisplay(); 

		if (clickToToggle)
		{
			clickToToggle = false; 
			ToggleSwitch(); 
		}

		if (useTimer > 0)
		{
			useTimer -= Time.deltaTime; 
			if (useTimer <= 0)
			{
				useTimer = 0; 
				activated = false; 
				SwitchInteract(); 
			}
		}

		if (activated)
		{
			progressBar.transform.localScale = new Vector3 (useTimer / useTime, progressBar.transform.localScale.y, progressBar.transform.localScale.z); 
		}
		else
		{
			progressBar.transform.localScale = new Vector3 (0, progressBar.transform.localScale.y, progressBar.transform.localScale.z); 
		}
	}

	public override void ToggleSwitch()
	{
		if (!activated)
		{
			activated = true;
			SwitchInteract(); 
			useTimer = useTime; 
		}
	}
}
