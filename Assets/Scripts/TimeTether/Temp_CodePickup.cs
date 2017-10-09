using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

[ExecuteInEditMode]
public class Temp_CodePickup : MonoBehaviour 
{
	public int codeNum; 
	public Text codeText; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (codeText != null)
		{
			codeText.text = "" + codeNum; 
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			//Temp_LevelStateManager.inst.playerNumKeys++; 
			//Destroy(this.gameObject); 
			if (codeNum >= 0 && codeNum < Temp_LevelStateManager.inst.codesFound.Length)
			{
				Temp_LevelStateManager.inst.codesFound[codeNum] = true;
			}

			gameObject.SetActive(false); 
		}
	}
}
