using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	/* Static Vars */
	public static GameManager manager { get; private set; }

	/* Instance Vars */

	// The overview camera that is visible at the beginning of the level
	[SerializeField]
	private Camera overviewCam;

	// The cameras used by players in the game
	[SerializeField]
	private Camera[] playerCams;

	/* Instance Methods */
	public void Awake()
	{
		if (manager == null)
			manager = this;
		else
			Debug.LogError ("More than on GameManager in the scene!");

		overviewCam.enabled = true;
		foreach (Camera c in playerCams)
			c.enabled = false;
	}

	public void swapFromOverviewToGameplay()
	{
		overviewCam.enabled = false;
		overviewCam.gameObject.SetActive (false);
		foreach (Camera c in playerCams)
			c.enabled = true;
	}
}
