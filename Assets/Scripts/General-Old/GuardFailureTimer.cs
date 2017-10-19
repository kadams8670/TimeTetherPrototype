using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuardFailureTimer : MonoBehaviour
{

	public float levelResetTimer = 99;
	private float curResetTimer; 

	private Text timertext;
	public bool timerIsActive=true;

	void OnEnable()
	{
		SaveStateManager.OnTetherStateLoaded += ResetFailureTimer;
	}

	void OnDisable()
	{
		SaveStateManager.OnTetherStateLoaded -= ResetFailureTimer; 
	}

	// Use this for initialization
	void Start () 
	{
		timertext = GetComponent<Text> ();
		curResetTimer = levelResetTimer; 
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timerIsActive) 
		{
			curResetTimer -= Time.deltaTime;
			timertext.text = curResetTimer.ToString ("f0");
			print (curResetTimer);
			if (curResetTimer <= 0) 
			{
				curResetTimer = 0;
				timerIsActive = false;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				print ("failed to save partner");
			}
		}	
	}

	void ResetFailureTimer()
	{
		timerIsActive = false; 
		curResetTimer = levelResetTimer; 
	}
}
