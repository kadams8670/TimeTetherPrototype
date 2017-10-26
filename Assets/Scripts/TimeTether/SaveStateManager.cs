using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SaveStateManager : Singleton<SaveStateManager> 
{
	[System.Serializable]
	public struct SaveState
	{
		// Basic GameObject data
		public SavableGameObjectData[] gameObjectData; 

		// Other struct types
		public TimedSimpleSwitchData[] timedSimpleSwitchData; 

		// LevelStateManager data
		public int playerNumKeys; 
		public bool[] codesFound; 
		public bool securityAlertActive; 
		public float endGoalTimer; 
	}



	[System.Serializable]
	public struct SavableGameObjectData
	{
		public GameObject gameObject; 
		public Vector3 position; 
		public Quaternion rotation; 
		public bool enabled; 
	}

	[System.Serializable]
	public struct TimedSimpleSwitchData
	{
		public TimedSimpleSwitch timedSimpleSwitch; 
		public bool activated; 
		public float useTimer; 
	}







	public GameObject levelParent; 

	public KeyCode saveKey; 
	public KeyCode jumpBackKey; 

	public int numSaveStates; 
	public int curSaveState; 

	[SerializeField] SaveState startState; 
	[SerializeField] SaveState[] saveStates; 

	// UI
	public Image[] ui_tetherPoints; 
	public Color ui_pointActiveColor; 
	public Color ui_pointInactiveColor; 
	public Color ui_pointStasisColor; 
	public GameObject curTimeArrow; 
	public bool arrowReachedPointTarget; 

	public Player player; 
	Vector3 playerSavedPos; 
	public bool playerMoved;  

	// Actions
	public static System.Action OnTetherStateSaved;
	public static System.Action OnTetherStateLoaded;

	// Stasis Stuff
	public List<GameObject> stasisBubbles; 

	// Use this for initialization
	void Start () 
	{
		// Create the array with one extra index to store the 0 level-start state
		saveStates = new SaveState[numSaveStates]; 

		startState = CreateSaveState(); 
		curSaveState = 0; 

		arrowReachedPointTarget = false; 
		playerMoved = false; 
		playerSavedPos = player.transform.position; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(saveKey) && curSaveState < numSaveStates - stasisBubbles.Count)
		{
			saveStates[curSaveState] = CreateSaveState(); 

			// Increment curSaveState index
			curSaveState++; 

			arrowReachedPointTarget = false; 
			playerMoved = false; 
			playerSavedPos = player.transform.position; 
		}

		if (Input.GetKeyDown(jumpBackKey))
		{
			// Decrement curSaveState index
			if (!playerMoved && curSaveState > 0)
			{
				curSaveState--; 
			}

			if (curSaveState <= 0)
			{
				LoadSaveState(startState);
			}
			else
			{
				Debug.Log("load curSaveState: " + curSaveState); 

				LoadSaveState(saveStates[curSaveState - 1]);
			}

			arrowReachedPointTarget = false; 
			playerMoved = false; 
			playerSavedPos = player.transform.position; 

			player.GetComponent<Rigidbody2D>().velocity = Vector3.zero; 
		}



		UpdateUI(); 
	}


	void UpdateUI()
	{
		if (ui_tetherPoints.Length > 0)
		{
			//ui_tetherPoints[0].color = ui_pointActiveColor; 
		}


		for (int i = 1; i < ui_tetherPoints.Length; i++)
		{
			
			if (i <= curSaveState)
			{
				ui_tetherPoints[i].color = ui_pointActiveColor; 
			}
			else
			{
				ui_tetherPoints[i].color = ui_pointInactiveColor;
			}

		}

		RectTransform rp = curTimeArrow.GetComponent<RectTransform>(); 

		float xTarget; 

		if (!arrowReachedPointTarget)
		{
			xTarget = -675 + (100 * curSaveState); 
		}
		else
		{
			xTarget = -625 + (100 * curSaveState); 
		}

		float xLerp = Mathf.Lerp(rp.anchoredPosition.x, xTarget, 0.2f); 
		rp.anchoredPosition = new Vector2 (xLerp, rp.anchoredPosition.y); 

		if (playerMoved && !arrowReachedPointTarget && Mathf.Abs(rp.anchoredPosition.x - xTarget) < 0.01f)
		{
			arrowReachedPointTarget = true; 
		}

		if (!playerMoved && Vector3.Distance(player.transform.position, playerSavedPos) > 0.1f)
		{
			playerMoved = true; 
		}

		// Update ui_tetherPoints to account for stasis
		for (int i = 0; i < stasisBubbles.Count; i++)
		{
			ui_tetherPoints[ui_tetherPoints.Length - 1 - i].color = ui_pointStasisColor; 
		}

	}



	public SaveState CreateSaveState()
	{
		SaveState writeState = new SaveState(); 

		// Check for error case
		if (curSaveState >= numSaveStates)
		{
			Debug.LogError("Can't make a save state. All slots filled"); 
			return writeState; 
		}

		// Make the save state

		// First, find all gameObjects in the scene with SavableGameObject script; those are affected

		// This searches/compiles all gameObjects each time
		SavableGameObject[] gs = levelParent.GetComponentsInChildren<SavableGameObject>(); 

		// Set the length of gameObjectData to the length of the number of GameObjects found in gs
		writeState.gameObjectData = new SavableGameObjectData[gs.Length];

		// For each gameObject found, add their data to the associated SaveableGameObjectData struct
		for (int i = 0; i < gs.Length; i++)
		{
			writeState.gameObjectData[i] = new SavableGameObjectData (); 
			writeState.gameObjectData[i].gameObject = gs[i].gameObject; 
			writeState.gameObjectData[i].position = gs[i].transform.position; 
			writeState.gameObjectData[i].rotation = gs[i].transform.rotation; 
			writeState.gameObjectData[i].enabled = gs[i].gameObject.activeSelf;
		}



		// Do something similar for timed switches
		TimedSimpleSwitch[] ts = levelParent.GetComponentsInChildren<TimedSimpleSwitch>(); 

		writeState.timedSimpleSwitchData = new TimedSimpleSwitchData[ts.Length]; 

		for (int i = 0; i < ts.Length; i++)
		{
			writeState.timedSimpleSwitchData[i] = new TimedSimpleSwitchData (); 
			writeState.timedSimpleSwitchData[i].timedSimpleSwitch = ts[i]; 
			writeState.timedSimpleSwitchData[i].activated = ts[i].activated;
			writeState.timedSimpleSwitchData[i].useTimer = ts[i].useTimer; 
		}




		// LevelStateManager data
		writeState.playerNumKeys = Temp_LevelStateManager.inst.playerNumKeys; 
		writeState.codesFound = Temp_LevelStateManager.inst.codesFound; 
		writeState.securityAlertActive = Temp_LevelStateManager.inst.securityAlertActive; 
		writeState.endGoalTimer = Temp_LevelStateManager.inst.endGoalTimer; 

		if (OnTetherStateSaved != null)
		{
			OnTetherStateSaved(); 
		}

		return writeState; 

	}

	public void LoadSaveState(SaveState readState)
	{
		// Load the saved data

		// Reset gameObjects to previous state
		foreach (SavableGameObjectData g in readState.gameObjectData)
		{
			//g.gameObject.transform.position = g.position; 
			//g.gameObject.transform.rotation = g.rotation;  
			//g.gameObject.SetActive(g.enabled); 
			g.gameObject.GetComponent<SavableGameObject>().SetGameObjectData(g); 
		}

		// Reset timed simple switches
		foreach (TimedSimpleSwitchData t in readState.timedSimpleSwitchData)
		{
			t.timedSimpleSwitch.activated = t.activated; 
			t.timedSimpleSwitch.useTimer = t.useTimer; 
		}

		// LevelStateManager data
		Temp_LevelStateManager.inst.playerNumKeys = readState.playerNumKeys; 
		Temp_LevelStateManager.inst.securityAlertActive = readState.securityAlertActive; 
		Temp_LevelStateManager.inst.endGoalTimer = readState.endGoalTimer; 

		if (OnTetherStateLoaded != null)
		{
			OnTetherStateLoaded(); 
		}
	}

	public bool CanMakeStasisBubble()
	{
		int remainingStates = numSaveStates - curSaveState; 
		remainingStates -= stasisBubbles.Count; 

		if (remainingStates > 0)
		{
			return true; 
		}

		return false; 
	}

	public void ResetStasisBubbles()
	{
		foreach (GameObject bubble in stasisBubbles)
		{
			Destroy(bubble); 
		}

		stasisBubbles.Clear(); 
	}
}
