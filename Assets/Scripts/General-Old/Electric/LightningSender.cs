using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSender : MonoBehaviour 
{
	public GameObject bullet;
	public bool isOn;
	private float timer;
	public float delay;
	private SpriteRenderer sprite;
	private Transform spawn;

	// Use this for initialization
	void Start () 
	{
		spawn = transform.Find ("spawn");
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isOn)
			sprite.color = Color.yellow;
		else
			sprite.color = Color.grey;
		if(timer <= 0)
		{
			Shoot ();
			timer = delay;
		}
		timer -= Time.deltaTime;
	}

	public void Shoot()
	{
		if (!isOn)
			return;
		Instantiate (bullet, spawn.position, spawn.rotation);
	}
}
