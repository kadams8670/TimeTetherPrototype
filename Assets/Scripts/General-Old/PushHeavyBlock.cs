using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushHeavyBlock : MonoBehaviour 
{
	public bool isHeavy;
	private GameObject character;
	[HideInInspector]
	public bool isBeingMoved;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player"))
			character = col.gameObject;
		else
			return;
		Player playerScript = character.GetComponent<Player> ();
		Itemgrabber grabber = character.GetComponent<Itemgrabber> ();
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player"))
			character = col.gameObject;
		else
			return;

		Player playerScript = character.GetComponent<Player> ();
		Itemgrabber grabber = character.GetComponent<Itemgrabber> ();
	}
}
		