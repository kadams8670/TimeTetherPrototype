using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGlove : MonoBehaviour 
{
	public bool push; 
	public KeyCode useKey; 
	public float strength; 
	public float range; 
	public float distMultiplier = 1; 
	public float multiplierExp = 1; 

	public ParticleSystem pushParticles; 
	public ParticleSystem pullParticles; 

	public LayerMask magLayers; 

	public float lockRotation = 999; //999 is lock value 

	Rigidbody2D rb; 

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(useKey))
		{
			if (lockRotation == 999)
			{
				lockRotation = transform.rotation.eulerAngles.z; 
			}
			else
			{
				transform.rotation = Quaternion.Euler(new Vector3 (0, 0, lockRotation)); 
			}

			if (push)
			{
				pushParticles.gameObject.SetActive(true); 
				pullParticles.gameObject.SetActive(false); 
			}
			else
			{
				pullParticles.gameObject.SetActive(true); 
				pushParticles.gameObject.SetActive(false); 
			}

			ObjectInteraction(); 

		}
		else
		{
			lockRotation = 999;
			pullParticles.gameObject.SetActive(false); 
			pushParticles.gameObject.SetActive(false); 
		}
	}

	void ObjectInteraction()
	{
		Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.position, range, magLayers.value);
	

		if(colliders.Length > 0)
		{
			// enemies within 1m of the player
			foreach (Collider2D col in colliders)
			{
				Debug.Log("Found " + col.gameObject.name); 

				if (!col.gameObject.GetComponent<MagneticObject>().Equals(null))
				{
					Rigidbody2D colrb = col.gameObject.GetComponent<Rigidbody2D>(); 
					//colrb.AddForce(new Vector2 (strength, 0)); 

					//var dir = transform.position - col.transform.position;
					//var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
					//col.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

					float dist = Vector2.Distance(transform.position, col.transform.position); 
					float distPerc = 1; 


					if (range != 0)
					{
						// Range = 10, dist = 8; 
						distPerc = (range - dist) / range; 
						//Debug.Log("distPerc: " + distPerc); 
					}

					//Debug.Log("distPerc: " + distPerc); 
					Vector2 dir = transform.position - col.transform.position;
					dir = dir.normalized;
					//Vector2 moveForce = dir * (strength + (distMultiplier * distPerc)); 
					Vector2 moveForce = dir * (strength + Mathf.Pow(distMultiplier * distPerc, multiplierExp)); 

					if (push)
					{
						colrb.AddForce(-moveForce); 
					}
					else
					{
						if (dist > 1.2f)
						{
							colrb.AddForce(moveForce);
						}
						else
						{
							colrb.velocity = new Vector2(0, 0); 
						}
					}


				}
			}
		}
	}

	/*
	void OnCollisionEnter2D(Collision2D col) 
	{
		if (col.gameObject.GetComponent<MagneticObject>() != null)
		{
			col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0); 

			rb.velocity = new Vector2 (0, 0); 
			Debug.Log("Collision"); 
		}

	}
	*/ 
	
}
