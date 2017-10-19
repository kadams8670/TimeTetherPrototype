using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour 
{
	public bool activated; 
	public float speed = 1.0f;

	void OnTriggerStay2D(Collider2D col)
	{
		if (activated)
		{
			Rigidbody2D rig;

			rig = col.gameObject.GetComponent<Rigidbody2D>();

			rig.AddForce(Vector3.right * speed * Time.deltaTime);
			print("SHIT WORKS");
		}
	}



}
