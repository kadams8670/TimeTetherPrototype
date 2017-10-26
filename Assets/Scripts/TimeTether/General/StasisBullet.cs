using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisBullet : MonoBehaviour 
{
	Rigidbody2D rb; 

	public float speed; 
	public LayerMask bulletLayerMask; 

	public GameObject stasisFieldPrefab; 

	public float durationTimer; 

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>(); 
	}


	// Update is called once per frame
	void Update () 
	{
		//rb.AddForce(new Vector2 (speed, 0), ForceMode2D.Impulse); 
		//rb.velocity = new Vector2(speed, 0); 

		rb.velocity = transform.up * speed; 

		durationTimer -= Time.deltaTime; 
		if (durationTimer <= 0)
		{
			Destroy(gameObject); 
		}
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		/*
		if(((1<<col.gameObject.layer) & bulletLayerMask) != 0)
		{
			Instantiate(stasisFieldPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject); 
		}
		*/ 

		if (SaveStateManager.inst.CanMakeStasisBubble())
		{
			SaveStateManager.inst.stasisBubbles.Add(Instantiate(stasisFieldPrefab, transform.position, Quaternion.identity));
		}

		Destroy(gameObject);
	}


	/*
	void OnTriggerEnter2D(Collider2D col)
	{
		if (((1 << col.gameObject.layer) & bulletLayerMask) != 0)
		{
			Instantiate(stasisFieldPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject); 
		}
	}
	*/ 

}
