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
	}

	public void SetAlertState(bool newState)
	{
		securityAlertActive = newState; 

		// GameObject iteration
	}
}
