using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

[ExecuteInEditMode]
public class CodeDoor : MonoBehaviour 
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

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			if (codeNum >= 0 && codeNum < Temp_LevelStateManager.inst.codesFound.Length)
			{
				if (Temp_LevelStateManager.inst.codesFound[codeNum])
				{
					gameObject.SetActive(false); 
				}
			}
		}
	}
}
