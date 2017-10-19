using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour {

	public GameObject player;
	public float range = 8;
	public float launchSpeed;
	public KeyCode launchKey;

	public bool canGrabItems;
	public bool canPullPlayer;

	[HideInInspector]
	public bool isExtended;
	[HideInInspector]
	public bool isRetracting;
	[HideInInspector]
	public bool isPullingPlayer;

	private LineRenderer rope;
	private bool hasItem;
	private Vector3 rotationFacing;
	private Vector3 targetLocation;

	// Use this for initialization
	void Start () 
	{
		if (player == null)
			player = transform.GetComponentInParent<Player> ().gameObject;
		rope = gameObject.GetComponent<LineRenderer> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if(!isExtended)
		{
			rotationFacing = player.transform.eulerAngles;
			transform.localPosition = Vector3.zero;
		}
		transform.eulerAngles = rotationFacing;
		rope.SetPosition (0, transform.position);
		rope.SetPosition (1, player.transform.position);
		if(Input.GetKeyDown(launchKey) && !isExtended)
		{
			targetLocation = (player.transform.position + player.transform.up * range);
			transform.SetParent (null);
			isExtended = true;
		}

		if(isExtended && !isRetracting && !isPullingPlayer)
		{
			if (Vector3.Distance (transform.position, player.transform.position) < range && Vector3.Distance(transform.position, targetLocation) > 0.1f)
			{
				Vector3 diff = transform.position - targetLocation;
				diff.Normalize ();

				float rotation = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (0f, 0f, rotation + 90);

				transform.Translate (Vector3.up * launchSpeed * 0.7f * Time.deltaTime);
			}
			else
				isRetracting = true;
		}
		if(isExtended && isRetracting)
		{
			if(Vector3.Distance(transform.position, player.transform.position) <= 0.4f)
			{
				if(hasItem)
				{
					hasItem = false;
					transform.DetachChildren ();
				}
				isExtended = false;
				isRetracting = false;
				transform.SetParent (player.transform);
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.identity;
			}
			else
			{
				Vector3 diff = transform.position - player.transform.position;
				diff.Normalize ();

				float rotation = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (0f, 0f, rotation + 90);

				transform.Translate (Vector3.up * launchSpeed * 0.7f * Time.deltaTime);
			}
		}
		if(isExtended && isPullingPlayer)
		{
			player.GetComponent<Player> ().canMove = false;

			Vector3 diff = player.transform.position - transform.position;
			diff.Normalize ();

			float rotation = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
			player.transform.rotation = Quaternion.Euler (0f, 0f, rotation + 90);

			player.transform.Translate (Vector3.up * launchSpeed * 0.7f * Time.deltaTime);

			if(Vector3.Distance(transform.position, player.transform.position) <= 0.4f)
			{
				player.GetComponent<Player> ().canMove = true;
				isExtended = false;
				player.layer = 13;
				isPullingPlayer = false;
				transform.SetParent (player.transform);
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.identity;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		//print (coll.gameObject.name);
		if (isExtended && !isRetracting && !isPullingPlayer)
		{
			if(coll.collider.CompareTag("Hook") && canPullPlayer)
			{
				player.layer = 20;
				isPullingPlayer = true;
			}
			else if(coll.collider.CompareTag("PullBlock"))
			{
				isRetracting = true;
				float xDist = coll.transform.position.x - player.transform.position.x; //if xDist is negative, player is right, else player is left
				float yDist = coll.transform.position.y - player.transform.position.y; //if xDist is negative, player is up, else player is down
				PullBlock block = coll.gameObject.GetComponent<PullBlock> ();

				if(Mathf.Abs(xDist) > Mathf.Abs(yDist))
				{
					//horizontal movement
					if(xDist < 0)
					{
						//moveRight
						block.Pull(PullBlock.Directions.RIGHT);
					}
					else
					{
						//moveLeft
						block.Pull(PullBlock.Directions.LEFT);
					}
				}
				else
				{
					//vertical movement
					if(yDist < 0)
					{
						//moveUp
						block.Pull(PullBlock.Directions.UP);
					}
					else
					{
						//moveDown
						block.Pull(PullBlock.Directions.DOWN);
					}
				}
			}
			else if(coll.collider.CompareTag("Item") && canGrabItems)
			{
				isRetracting = true;
				hasItem = true;
				coll.collider.transform.SetParent (transform);
			}
			else
			{
				isRetracting = true;
			}
		}

	}
}
