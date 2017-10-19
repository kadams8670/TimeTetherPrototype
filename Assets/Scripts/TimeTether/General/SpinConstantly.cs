using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinConstantly : MonoBehaviour 
{
	public Vector3 spinSpeed; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(spinSpeed * Time.deltaTime); 
	}
}
