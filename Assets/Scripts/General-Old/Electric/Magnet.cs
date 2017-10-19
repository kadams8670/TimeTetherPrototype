using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour 
{
	public bool activated; 
	public float strength; 
	public bool push; 

	public float range; 
	public float angleRange; 
	public float angleStep = 1; 

	ParticleSystem pushParticles; 
	ParticleSystem pullParticles; 


	// Use this for initialization
	void Start () 
	{
		pushParticles = transform.Find("PushParticles").GetComponent<ParticleSystem>(); 
		pullParticles = transform.Find("PullParticles").GetComponent<ParticleSystem>(); 
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//List<Rigidbody2D> affected = new List<Rigidbody2D>(); 
		//List<Rigidbody2D> affected = FindAffectedObjects(); 

		if (activated)
		{
			// Particles
			if (push)
			{
				pushParticles.gameObject.SetActive(true); 
				pullParticles.gameObject.SetActive(false); 
			}
			else
			{
				pushParticles.gameObject.SetActive(false); 
				pullParticles.gameObject.SetActive(true);
			}

			List<Rigidbody2D> affected = FindAffectedObjects(); 
			/*
			// Find objects within range
			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, range); 
			//Debug.DrawRay(transform.position, -transform.up * range); 

			Debug.Log("hits: " + hits.Length); 

			for (int i = 0; i < hits.Length; i++)
			{
				if (hits[i].transform.gameObject.GetComponent<MagneticObject>())
				{
					affected.Add(hits[i].transform.gameObject.GetComponent<Rigidbody2D>()); 
				}
			}
			*/ 

			// Push/pull affected objects
			foreach (Rigidbody2D rb in affected)
			{
				Vector3 moveDir = (transform.position - rb.transform.position).normalized; 

				if (push)
				{
					if (rb.GetComponent<LightningBolt> () != null)
						rb.velocity = rb.GetComponent<LightningBolt> ().moveSpeed * rb.transform.up + (-transform.up * strength * rb.gameObject.GetComponent<MagneticObject> ().strengthMultiplier);
					else
						//rb.AddForce(-transform.up * strength * rb.gameObject.GetComponent<MagneticObject>().strengthMultiplier);
						rb.AddForce(moveDir * strength * rb.gameObject.GetComponent<MagneticObject>().strengthMultiplier);
				}
				else
				{
					if (rb.GetComponent<LightningBolt> () != null)
						rb.velocity = rb.GetComponent<LightningBolt> ().moveSpeed * rb.transform.up + (transform.up * strength * rb.gameObject.GetComponent<MagneticObject> ().strengthMultiplier);
					else
						//rb.AddForce(transform.up * strength * rb.gameObject.GetComponent<MagneticObject>().strengthMultiplier);
						rb.AddForce(moveDir * strength * rb.gameObject.GetComponent<MagneticObject>().strengthMultiplier);
				}

				/* Sarah's code for facing direction
				Vector3 diff = transform.position - target;
				diff.Normalize();

				float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0f, 0f, rotation + 90);

				transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
				*/ 
			}
		}
		else
		{
			pushParticles.gameObject.SetActive(false); 
			pullParticles.gameObject.SetActive(false); 
		}
	}

	public void Toggle()
	{
		activated = !activated; 
	}


	/// <summary>
	/// Returns a list of rigidbodies found by cone raycasts
	/// </summary>
	/// <returns>The list of affected rigidbodies.</returns>
	List<Rigidbody2D> FindAffectedObjects()
	{ 
		// Create a list to store any objects affected by the raycast
		List<Rigidbody2D> affected = new List<Rigidbody2D>();

		// Don't continue if these values might cause an infinite for loop
		if (angleStep <= 0 || angleRange <= 0)
		{
			return affected; 
		}

		// Calculate the start and end angles
		float startAngle = -(angleRange / 2); 
		float endAngle = angleRange / 2; 

		// Loop for each raycast, using a for the angle
		for (float a = startAngle; a <= endAngle; a += angleStep)
		{
			// Calculate the angle, relative to the magnet's facing direction (-transform.up)
			Vector3 adir = Quaternion.Euler(0, 0, a) * -transform.up; 

			// Visualize the ray (enable Gizmos in Game view)
			Debug.DrawRay(transform.position, adir * range);

			// Find objects within range
			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, adir, range);

			// Iterate through each hit found by the raycast
			for (int i = 0; i < hits.Length; i++)
			{
				// If the hit is valid (here determined by whether it has the MagneticObject script)
				if (hits[i].transform.gameObject.GetComponent<MagneticObject>())
				{
					Rigidbody2D curRb = hits[i].transform.gameObject.GetComponent<Rigidbody2D>(); 

					// Add the rigidbody within hit to affected if it isn't already in affected
					if (!affected.Contains(curRb))
					{
						affected.Add(curRb); 
					}
				}
			}
		}

		return affected; 
	}
}
