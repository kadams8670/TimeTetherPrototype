using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SavableGameObject))]
public class PermanentKeyPickup : MonoBehaviour 
{
	// Used to solve a glitch with the onTriggerEnter triggering twice
	bool collected = false; 

	SavableGameObject savableGameObject; 

	// Use this for initialization
	void Start () 
	{
		savableGameObject = GetComponent<SavableGameObject>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		collected = false; 
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") && !collected && !Temp_LevelStateManager.inst.permKeyFound)
		{
			collected = true; 
			Temp_LevelStateManager.inst.permKeyFound = true; 
			Temp_LevelStateManager.inst.permKeyPickup = this; 
			//Destroy(this.gameObject); 
			savableGameObject.SetLocked(true); 

			gameObject.SetActive(false); 
		}
	}

	public void Dropped(Vector3 pos)
	{
		collected = false; 
		savableGameObject.SetLocked(false); 
		transform.position = pos; 
	}

	public void Used()
	{
		savableGameObject.SetLocked(false); 
		gameObject.SetActive(false); 
	}

}
