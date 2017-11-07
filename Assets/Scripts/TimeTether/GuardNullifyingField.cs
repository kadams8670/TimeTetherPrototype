using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardNullifyingField : MonoBehaviour 
{
	GuardAI guard;
	Player playerGrab;
	GhostForm woooooo;
	bool isNullifying;

	
	// Update is called once per frame
	void Update () 
	{

	}
		
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<Collider2D> ().CompareTag ("Player"))
		{
			Debug.Log ("Im triggered");
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().canJump = false;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<GhostForm> ().isDisabled = true;
			GameObject.Find ("SaveStateManager").GetComponent<SaveStateManager> ().ResetStasisBubbles ();
			GameObject.FindGameObjectWithTag ("Player").GetComponent<GhostForm> ().ShiftFromGhost();
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<GhostForm> ().isDisabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().canJump = true;
	}
}
