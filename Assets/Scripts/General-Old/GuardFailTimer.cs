using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardFailTimer : MonoBehaviour 
{
	public void FailTimerActivated (float timeToChange)
	{
		StartCoroutine (ActivateSceneRoutine (timeToChange));
	}
		
	private IEnumerator ActivateSceneRoutine(float timeToChange)
	{
		yield return new WaitForSeconds(timeToChange);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
