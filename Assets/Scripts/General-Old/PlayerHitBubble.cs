using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBubble : MonoBehaviour 
{
	public float wakeUpTime= 5f;
	public float timetoresetLevel= 10f;
	public GameObject player;

	GuardAI guardHit; 

	void OnEnable()
	{
		SaveStateManager.OnTetherStateLoaded += ResetPlayerHit;
	}

	void OnDisable()
	{
		SaveStateManager.OnTetherStateLoaded -= ResetPlayerHit; 
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<Collider2D>().CompareTag ("Guard")) 
		{
			Debug.Log ("Collided");
			//GameObject.FindGameObjectWithTag("Guard").GetComponent<GuardAI>().enabled=false;

			guardHit = col.GetComponent<GuardAI>(); 
			guardHit.enabled = false; 

			player.GetComponent<Player> ().enabled = false;
			Debug.Log ("Activated Fail Timer");
			GameObject.FindGameObjectWithTag ("Guard").GetComponent<GuardFailTimer> ().FailTimerActivated(timetoresetLevel);
		}
	}

	/*
	void OnTriggerExit2D(Collider2D col)
	{
		
		if (col.GetComponent<Collider2D>().CompareTag("Player")) 
		{
			Debug.Log ("Player Saved");
			player.GetComponent<Player> ().enabled = true;
			GameObject.FindGameObjectWithTag ("Guard").GetComponent<GuardTimer> ().ActivateAfterCollide (wakeUpTime);
			Debug.Log("Stop Fail Timer");
			GameObject.FindGameObjectWithTag ("Guard").GetComponent<GuardFailTimer> ().StopAllCoroutines();
		}
	}
	*/ 

	void ResetPlayerHit()
	{
		player.GetComponent<Player> ().enabled = true;

		if (guardHit != null)
		{
			guardHit.enabled = true;
		}
	}
}
