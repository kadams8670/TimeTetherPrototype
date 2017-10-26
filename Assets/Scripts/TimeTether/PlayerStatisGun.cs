using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatisGun : MonoBehaviour 
{
	public KeyCode shootKey; 
	public GameObject stasisBulletPrefab; 

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(shootKey) && SaveStateManager.inst.CanShootStasis())
		{
			Shoot(); 
		}
	}

	void Shoot()
	{
		GameObject newBullet = Instantiate(stasisBulletPrefab, transform.position, Quaternion.identity, this.transform.parent);
		newBullet.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);  
	}
}
