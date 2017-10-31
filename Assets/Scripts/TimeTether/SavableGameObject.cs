﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavableGameObject : MonoBehaviour 
{
	[ReadOnly] public bool locked; 

	[Tooltip("When true, removing stasis will NOT reset the object position/rotation/enabled data. This is useful for stuff like moving object in the otherworld")]
	public bool dontResetUponUnlock; 

	public SaveStateManager.SavableGameObjectData lockData;


	public void SetGameObjectData(SaveStateManager.SavableGameObjectData objectData)
	{
		if (!locked)
		{
			transform.position = objectData.position; 
			transform.rotation = objectData.rotation; 
			gameObject.SetActive(objectData.enabled); 
		}
	}


	void SaveLockData()
	{
		lockData.position = transform.position; 
		lockData.rotation = transform.rotation; 
		lockData.enabled = gameObject.activeSelf; 
	}

	void ReadLockData()
	{
		transform.position = lockData.position; 
		transform.rotation = lockData.rotation; 
		gameObject.SetActive(lockData.enabled); 
	}

	public bool GetLocked()
	{
		return locked; 
	}

	public void SetLocked(bool lockState)
	{
		if (lockState == true)
		{
			EnableLock(); 
		}
		else
		{
			DisableLock(); 
		}
	}

	void EnableLock()
	{
		if (locked)
		{
			return; 
		}

		locked = true; 
		SaveLockData(); 
	}

	void DisableLock()
	{
		if (!locked)
		{
			return; 
		}

		locked = false; 

		if (!dontResetUponUnlock)
		{
			ReadLockData(); 
		}
	}
		
}
