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

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		keyText.text = "x" + playerNumKeys; 
	}

	public void SetAlertState(bool newState)
	{
		securityAlertActive = newState; 

		// GameObject iteration
	}
}
