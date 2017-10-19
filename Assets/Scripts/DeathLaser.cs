using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLaser : MonoBehaviour 
{
	public bool isOn;

	// Update is called once per frame
	void Update () 
	{
		if(isOn)
			gameObject.SetActive(true); 
		else
			gameObject.SetActive(false); 
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{ 
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		else if (col.CompareTag("Guard"))
		{ 
			isOn = false;
			col.gameObject.SetActive (false);
		}
	}

	public void TurnOn()
	{
		isOn = true;
	}

	public void TurnOff()
	{
		isOn = false;
	}

	public void SetState(bool state)
	{
		isOn = state;
	}

	public void Toggle()
	{
		isOn = !isOn;
	}
}
