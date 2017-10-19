using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryNode : MonoBehaviour 
{
	[Tooltip("The color of the BatteryNode sprite when on")]
	public Color onColor; 
	[Tooltip("The color of the BatteryNode sprite when off")]
	public Color offColor; 

	[HideInInspector] public bool activated; 

	[Tooltip("The length of time, in seconds, it takes to transfer energy to/from the battery node")]
	public float useTimerLength;
	private float useTimer; 

	SpriteRenderer rend; 

	GameObject progressBar; 

	[Header("Toggle via Inspector")]
	public bool clickToToggle; 

	void Awake ()
	{
		rend = GetComponent<SpriteRenderer>(); 
	}

	// Use this for initialization
	void Start () 
	{
		useTimer = useTimerLength; 

		SetColor(); 

		//if (transform.Find("ProgressBar").gameObject != null)
		if (transform.Find("ProgressBar") != null)
		{
			progressBar = transform.Find("ProgressBar").gameObject; 
			progressBar.transform.localScale = new Vector3 (0, progressBar.transform.localScale.y, progressBar.transform.localScale.z); 
		}
	}

	// Update is called once per frame
	void Update () 
	{
		/*
		if (justToggled && !Input.GetKey(KeyCode.RightShift))
		{
			justToggled = false; 
		}
		*/

		if (clickToToggle)
		{
			clickToToggle = false; 
			SetActivated(!activated); 
		}

		SetColor(); 
	}

	public bool Interact(bool willActivate)
	{
		if (useTimer < useTimerLength && useTimerLength != 0)
		{
			float scaleX = ((useTimer) / useTimerLength) * 2; 

			if (progressBar != null)
				progressBar.transform.localScale = new Vector3 (scaleX, progressBar.transform.localScale.y, progressBar.transform.localScale.z); 
		}
		if (useTimer > 0)
		{
			useTimer -= Time.deltaTime; 
			if (useTimer < 0)
			{
				useTimer = 0;
				//tempShockStat.charged = !tempShockStat.charged;

				progressBar.transform.localScale = new Vector3 (0, progressBar.transform.localScale.y, progressBar.transform.localScale.z); 
				SetActivated(willActivate); 
				return true; 
			}
		}
		return false; 
	}

	// Resets interaction if the player didn't finish
	public void ResetInteract()
	{
		useTimer = useTimerLength; 
		progressBar.transform.localScale = new Vector3 (0, progressBar.transform.localScale.y, progressBar.transform.localScale.z); 
	}
		


	public void SetActivated(bool _activated)
	{
		activated = _activated; 
		useTimer = useTimerLength; 
		SwitchInteract(); 
	}

	void SwitchInteract()
	{
		foreach (Interactable i in GetComponents<Interactable>())
		{
			i.OnInteract(); 
		}
	}

	void SetColor()
	{
		if (activated)
		{
			rend.color = onColor; 
		}
		else
		{
			rend.color = offColor; 
		}
	}


}
