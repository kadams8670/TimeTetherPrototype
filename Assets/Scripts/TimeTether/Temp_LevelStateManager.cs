using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Temp_LevelStateManager : Singleton<Temp_LevelStateManager> 
{
	public int playerNumKeys; 
	public bool[] codesFound; 

	public Text keyText; 
	public Text codesText; 

	public bool securityAlertActive; 
	public GameObject alertIcon; 

	public float endGoalTimer; 
	public Text endGoalTimerText; 

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		keyText.text = "x" + playerNumKeys; 

		codesText.text = ""; 
		for (int i = 0; i < codesFound.Length; i++)
		{
			if (codesFound[i])
			{
				codesText.text += "(" + i + ") "; 
			}
		}

		if (securityAlertActive)
		{
			alertIcon.SetActive(true); 
		}
		else
		{
			alertIcon.SetActive(false); 
		}

		if (endGoalTimer > 0)
		{
			endGoalTimer -= Time.deltaTime; 
			if (endGoalTimer <= 0)
			{
				endGoalTimer = 0; 
				SwitchInteract();
			}
		}

		if (endGoalTimerText != null)
		{
			endGoalTimerText.text = "Goal time left: " + Mathf.FloorToInt(endGoalTimer); 
		}
	}

	public void SetAlertState(bool newState)
	{
		securityAlertActive = newState; 

		// GameObject iteration
	}

	protected virtual void SwitchInteract()
	{
		foreach (Interactable i in GetComponents<Interactable>())
		{
			i.OnInteract(); 
		}
	}
}
