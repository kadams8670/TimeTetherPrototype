using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TeleportUI : MonoBehaviour
{
	[SerializeField]
	private Image cdImage;
	public Player subject;

	public void Update()
	{
		if (cdImage != null)
			cdImage.fillAmount = subject.cooldownPercentage ();
	}
}
