using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointSwitch : SimpleSwitch 
{
	[Tooltip("Drag in the other JointSwitch paired to this one")]
	public JointSwitch pairedSwitch; 

	[Tooltip("How much time does the other player have to hit their switch. It is recommended to keep this value the same for both JointSwitches")]
	public float waitTimerLength; 
	float waitTimer; 

	protected override void Update()
	{
		UpdateSwitchDisplay(); 

		if (clickToToggle)
		{
			clickToToggle = false; 
			ToggleSwitch(); 
		}

		if (waitTimer > 0)
		{
			waitTimer -= Time.deltaTime; 

			if (waitTimer <= 0)
			{
				waitTimer = 0; 
			}

			// One switch takes care of doing these actions for both switches. 
			if (pairedSwitch.waitTimer > 0)
			{
				waitTimer = 0; 
				pairedSwitch.waitTimer = 0; 

				activated = !activated;
				pairedSwitch.activated = !pairedSwitch.activated; 

				SwitchInteract(); 
				pairedSwitch.SwitchInteract(); 

			}
		}
	}

	public override void ToggleSwitch()
	{
		Debug.Log("Toggle"); 

		waitTimer = waitTimerLength;  
	}


	protected override void UpdateSwitchDisplay()
	{
		if (activated)
		{
			transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); 
		}
		else
		{
			transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); 
		}
	}

	protected override void SwitchInteract()
	{
		foreach (Interactable i in GetComponents<Interactable>())
		{
			i.OnInteract(); 
		}
	}
}
