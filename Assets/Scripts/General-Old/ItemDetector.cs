using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetector : MonoBehaviour {

	public bool isActive = false;
	public string itemName = "key";
	private GameObject item;
	public bool destroysObject = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D colliderHit)
	{
		if (colliderHit.CompareTag ("Item") && !isActive && colliderHit.name == itemName)
		{
			isActive = true;
			item = colliderHit.gameObject;
			if (destroysObject)
				Destroy (item);
		}
		else if (colliderHit.CompareTag ("Player") && !isActive && colliderHit.transform.Find("ItemSlot").Find(itemName) != null)
		{
			isActive = true;

			item = colliderHit.transform.Find("ItemSlot").Find(itemName).gameObject;
			if (destroysObject)
				Destroy (item);
			else {
				item.transform.SetParent (null);
				item.GetComponent<CircleCollider2D> ().enabled = true;
			}
			colliderHit.GetComponent<Itemgrabber> ().hasItem = false;
		}
	}
}
