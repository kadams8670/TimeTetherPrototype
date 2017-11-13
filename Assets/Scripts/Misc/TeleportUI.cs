using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportUI : MonoBehaviour
{
	private GameObject[] chargeIndicators;
	public Player subject;

	public void Awake()
	{
		if (subject != null)
		{
			GameObject pref = Resources.Load<GameObject> ("ChargeCDIndicator");
			chargeIndicators = new GameObject[subject.maxCharges];
			for (int i = 0; i < subject.maxCharges; i++)
			{
				chargeIndicators [i] = Instantiate (pref, transform, false);
			}
		}
	}

	public void Update()
	{
		if (subject == null)
			return;

		for (int i = subject.maxCharges - 1; i >= 0; i--)
		{
			Image currImg = chargeIndicators [i].transform.GetChild (1).GetComponent<Image> ();
			if (i > subject.currCharges)
			{
				currImg.fillAmount = 0f;
			}
			else if (i == subject.currCharges)
			{
				currImg.fillAmount = 1 - subject.cooldownPerc;
			}
			else
			{
				currImg.fillAmount = 1f;
			}
		}
	}
}
