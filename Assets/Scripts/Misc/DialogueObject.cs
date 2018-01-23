using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : ScriptableObject 
{
	private bool _interactable;
	private DialogueEvent[] _events;
	private int _currentDialogue;

	public DialogueObject()
	{
		
	}

	public DialogueObject(bool interactable, DialogueEvent[] events)
	{
		_interactable = interactable;

	}

	public DialogueEvent next()
	{
		//TODO:: display next dialogue event
		//if interactable: pause game, and allow input
	}
}
