using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTimer : MonoBehaviour 
{

	public void ActivateAfterCollide (float time)
	{
		StartCoroutine (ActivateRoutine (time));
	}

	private IEnumerator ActivateRoutine(float time)
	{
		yield return new WaitForSeconds(time);
		GameObject.FindGameObjectWithTag ("Guard").GetComponent<GuardAI> ().enabled = true;
	}
}
