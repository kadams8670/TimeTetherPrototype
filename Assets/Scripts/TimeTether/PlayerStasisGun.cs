using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStasisGun : MonoBehaviour 
{
	public KeyCode shootKey; 
	public bool resetWhenFull; 

	public bool cancelResetsAllBubbles; 

	public KeyCode cancelKey; 
	public GameObject stasisBulletPrefab; 

	public float shootDelayLength = 0.2f; 
	float shootDelayTimer; 

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(shootKey) && shootDelayTimer <= 0)
		{
			if (SaveStateManager.inst.CanMakeStasisBubble())
			{
				Shoot(); 
			}
			else if (resetWhenFull)
			{
				SaveStateManager.inst.ResetStasisBubbles(); 
			}
		}

		if (Input.GetKeyDown(cancelKey))
		{
			if (cancelResetsAllBubbles)
			{
				SaveStateManager.inst.ResetStasisBubbles(); 
			}
			else
			{
				SaveStateManager.inst.RemoveLastBubble(); 
			}
		}

		if (shootDelayTimer > 0)
		{
			shootDelayTimer -= Time.deltaTime; 
			if (shootDelayTimer < 0)
				shootDelayTimer = 0; 
		}
	}

	void Shoot()
	{
		Debug.Log("Shoot"); 
		shootDelayTimer = shootDelayLength; 
		GameObject newBullet = Instantiate(stasisBulletPrefab, transform.position, Quaternion.identity, this.transform.parent);
		newBullet.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);  
	}
}
