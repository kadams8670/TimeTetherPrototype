using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemgrabber : MonoBehaviour 
{

	public bool hasItem = false;
	public float itemReach = 1.5f;
	public LayerMask itemLayer;
	public KeyCode grabButton;
	public GameObject itemSlot;
	public PushHeavyBlock boxToGrab;
	public PushHeavyBlock boxBeingMoved;
	
	// Update is called once per frame
	void Update () {
		ItemGrabber ();
	}

	void ItemGrabber()
	{
		Player playerScript = GetComponent<Player> ();

		if(Input.GetKeyDown(grabButton))
		{
			Collider2D item = Physics2D.OverlapCircle (transform.position, itemReach, itemLayer);
			if(boxToGrab != null && boxBeingMoved == null)
			{
				boxToGrab.GetComponent<Rigidbody2D> ().simulated = false;
				boxToGrab.transform.SetParent (transform);
				boxBeingMoved = boxToGrab;
				if (boxBeingMoved.isHeavy)
					playerScript.movespeed.setBase (8);
				if (!boxBeingMoved.isHeavy)
					playerScript.movespeed.setBase (10);
			}
			else if(boxBeingMoved != null)
			{
				boxBeingMoved.GetComponent<Rigidbody2D> ().simulated = true;
				boxBeingMoved.transform.SetParent (null);
				boxBeingMoved = null;
			}
			else if(item != null && !hasItem)
			{
				item.gameObject.transform.SetParent (itemSlot.transform);
				hasItem = true;
				item.gameObject.transform.localPosition = Vector3.zero;
				item.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			}
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			 if(hasItem)
			{
				hasItem = false;
				playerScript.movespeed.setBase (17);
				Transform item = itemSlot.transform.GetChild (0);
				item.GetComponent<CircleCollider2D> ().enabled = true;
				item.SetParent (null);
				item.GetComponent<Rigidbody2D> ().velocity = transform.forward * 10;
			}
		}
	}
}
