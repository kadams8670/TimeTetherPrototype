using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuardFailureTimer : MonoBehaviour
{

	public float levelResetTimer = 99;
	private Text timertext;
	public bool timerIsActive=true;

	// Use this for initialization
	void Start () 
	{
		timertext = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timerIsActive) 
		{
			levelResetTimer -= Time.deltaTime;
			timertext.text = levelResetTimer.ToString ("f0");
			print (levelResetTimer);
			if (levelResetTimer <= 0) 
			{
				levelResetTimer = 0;
				timerIsActive = false;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				print ("failed to save partner");
			}
		}	
	}
}
