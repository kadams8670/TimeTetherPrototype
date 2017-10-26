using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostForm : MonoBehaviour 
{
	public bool formActive = false;
	public KeyCode useKey;
	public float duration = 5f;
	public float timer = 0;
	public GameObject shell;
	private GameObject spawnedShell;

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
			timer-=Time.deltaTime;
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
		//new Color(0.25f, 1f, 0.6f, 0.2f);
		gameObject.GetComponent<SpriteRenderer> ().color = new Color(0.25f, 1f, 0.6f, 0.2f);;
		spawnedShell = Instantiate (shell, transform.position, transform.rotation, transform.parent);
	}

	void ShiftFromGhost()
	{
		gameObject.layer = 13;
		timer = 0;
		formActive = false;
		gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		Destroy (spawnedShell);
	}
}
