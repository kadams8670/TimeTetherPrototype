using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEndDoor : MonoBehaviour 
{
	public GameObject door; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Temp_LevelStateManager.inst.endGoalTimer <= 0)
		{
			door.SetActive(true); 
		}
		else
		{
			door.SetActive(false); 
		}
	}
}
