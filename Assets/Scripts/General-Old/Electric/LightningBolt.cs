using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour 
{
	public float moveSpeed;
	public Rigidbody2D rb2d;
	public float checkRadius =  0.6f;

	//private float currentYVelocity;
	//private float currentXVelocity;
	//public bool canBePulled;
	private MagneticObject magnetSript;

	// Use this for initialization
	void Start () {
		magnetSript = gameObject.GetComponent<MagneticObject> ();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		rb2d.velocity = transform.up * moveSpeed;

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate()
	{
		
	}

	void OnCollisionEnter2D(Collision2D colliderHit)
	{
		if (colliderHit.gameObject.GetComponent<LightningReciever> () != null)
		{
			colliderHit.gameObject.GetComponent<LightningReciever> ().Charge ();
		}
		Destroy (gameObject);
	}

}
