using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostForm : MonoBehaviour 
{
	public bool formActive = false;
	public KeyCode useKey;
	public float duration = 5f;
	private float timer = 0;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(useKey))
			Shift ();
		if(formActive)
		{
			timer--;
			if(timer <= 0)
			{
				ShiftFromGhost ();
			}
		}
	}

	void Shift()
	{
		if (formActive)
			ShiftFromGhost ();
		else
			ShiftToGhost ();
	}

	void ShiftToGhost()
	{
		gameObject.layer = 14;
		timer = duration;
		formActive = true;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color(0.25f, 1f, 0.6f, 0.2f);
	}

	void ShiftFromGhost()
	{
		gameObject.layer = 13;
		timer = 0;
		formActive = false;
		gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
	}
}
