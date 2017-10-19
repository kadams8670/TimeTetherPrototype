using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningReciever : MonoBehaviour {

	private float timer = 0;
	private bool isActive;
	public float delay = 0.25f;
	private SpriteRenderer sprite;
	[HideInInspector]
	public bool activated;

	// Use this for initialization
	void Start () 
	{
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isActive && !activated)
		{
			activated = true;
			SwitchInteract ();
		}
		if(!isActive && activated)
		{
			activated = false;
			SwitchInteract ();
		}
		if (timer > 0)
			timer -= Time.deltaTime;
		else
			timer = 0;
		isActive = (timer > 0);

		if (isActive)
			sprite.color = Color.yellow;
		else
			sprite.color = Color.grey;
	}

	public void Charge()
	{
		timer = delay;
	}

	void SwitchInteract()
	{
		foreach (Interactable i in GetComponents<Interactable>())
		{
			i.OnInteract(); 
		}
	}
}
