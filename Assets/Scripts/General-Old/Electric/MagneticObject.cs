using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObject : MonoBehaviour 
{
	Rigidbody rb; 
	public float strengthMultiplier; 

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>(); 
		//Physics2D.IgnoreCollision(GameObject.Find("Players/Infil").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		//Physics2D.IgnoreCollision(GameObject.Find("Players/Hacker").GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
