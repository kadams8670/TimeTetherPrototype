using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBlock : MonoBehaviour 
{
	public bool canMoveUp;
	public bool canMoveRight;
	public bool canMoveLeft;
	public bool canMoveDown;
	public float pushForce;

	public enum Directions {UP, RIGHT, LEFT, DOWN};
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () 
	{

	}

	public void Pull(Directions direction)
	{
		gameObject.layer = 13;

		switch(direction)
		{
		case Directions.UP:
			rb2d.AddForce (transform.up * pushForce);
			break;
		case Directions.RIGHT:
			rb2d.AddForce (transform.right * pushForce);
			break;
		case Directions.LEFT:
			rb2d.AddForce (transform.right * -1 * pushForce);
			break;
		case Directions.DOWN:
			rb2d.AddForce (transform.up * -1 * pushForce);
			break;
		}
		StartCoroutine (ResetLayer ());
	}

	IEnumerator ResetLayer()
	{
		yield return new WaitForSeconds (0.75f);
		gameObject.layer = 12;
	}
}
