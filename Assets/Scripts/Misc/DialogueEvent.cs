using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEvent : ScriptableObject
{
	private Sprite _sprite;
	private string _name = "DIALOGUE_TITLE";
	private string _text = "DIALOGUE_TEXT";
	private AudioClip _gibberish;
	private float _printDelay = 0;
	private float _paddingTime = 0;

	public DialogueEvent()
	{
		
	}

	public DialogueEvent(Sprite sprite, string name, string text, AudioClip gibberish, float printDelay, float paddingTime)
	{
		_sprite = sprite;
		_text = text;
		_gibberish = gibberish;
		_printDelay = printDelay;
		_paddingTime = paddingTime;
	}

	public Sprite Sprite
	{
		get{return _sprite;}
		set{_sprite = value;}
	}

	public string Name
	{
		get{return _name;}
		set{_name = value;}
	}

	public string Text
	{
		get{return _text;}
		set{_text = value;}
	}

	public AudioClip Gibberish
	{
		get{return _gibberish;}
		set{_gibberish = value;}
	}

	public float PrintDelay
	{
		get{return _printDelay;}
		set{_printDelay = value;}
	}

	public float PaddingTime
	{
		get{return _paddingTime;}
		set{_paddingTime = value;}
	}
}
