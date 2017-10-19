using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonFloor : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.CompareTag("Player"))
		{
			other.GetComponent<Chameleon> ().isInHideZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if(other.CompareTag("Player"))
		{
			other.GetComponent<Chameleon> ().isInHideZone = false;
		}
	}
}
