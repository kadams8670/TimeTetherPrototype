using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostForm : MonoBehaviour 
{
	public bool formActive = false;
	public KeyCode useKey;
	public KeyCode cancelKey;
	public float duration = 5f;
	public float rechargeRate = 1;
	[ReadOnly] public float timer = 0;
	public GameObject shell;
	private GameObject spawnedShell;
	public bool isDisabled = false; 
	private Image fillMeter;

	// Use this for initialization
	void Start () 
	{
		GameObject meter = GameObject.Find ("GHOSTMETER");
		if (meter != null)
			fillMeter = meter.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(fillMeter != null)
		{
			fillMeter.fillAmount = (timer / duration);
		}
		if(Input.GetKeyDown(useKey))
			Shift ();
		if(Input.GetKeyDown(cancelKey) && formActive)
			Return ();
		if(formActive)
		{
			timer-=Time.deltaTime;
			if(timer <= 0)
			{
				ShiftFromGhost ();
			}
		}
		else
		{
			if(timer < duration)
				timer += (Time.deltaTime * rechargeRate);
			else
				timer = duration;
		}
		if (isDisabled == true && formActive == true) 
		{
			ShiftFromGhost ();
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
		formActive = true;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color(0.25f, 1f, 0.6f, 0.2f);;
		spawnedShell = Instantiate (shell, transform.position, transform.rotation, transform.parent);
	}

	public void ShiftFromGhost()
	{
		gameObject.layer = 13;
		formActive = false;
		gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		Destroy (spawnedShell);
	}

	public void Return()
	{
		gameObject.layer = 13;
		formActive = false;
		gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		transform.position = spawnedShell.transform.position;
		transform.rotation = spawnedShell.transform.rotation;
		Destroy (spawnedShell);
	}
}
