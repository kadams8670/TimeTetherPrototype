using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : Interactable
{
//	/* Instance Vars */
//
//	// A single GameObject target that is toggled to match the activated state
//	[SerializeField]
//	private GameObject[] toggletargets;
//
//	[SerializeField]
//	private Console pairedConsole;
//
//	// A set of IInteractable objects that will be toggled whenever this Console is toggled
//	[SerializeField]
//	private List<Interactable> subInteractables;
//
//	// Colors this Interactable object toggles through when activated/deactivated
//	[SerializeField]
//	private Color activeColor;
//	[SerializeField]
//	private Color inactiveColor;
//
//	// Overridden activated field to allow for color toggling
//	public override bool activated
//	{
//		get{ return base.activated; }
//		protected set
//		{
//			base.activated = value;
//			GetComponent<SpriteRenderer> ().color = value ? activeColor : inactiveColor;
//		}
//	}
//
//	/* Instance Methods */
//	public void Awake()
//	{
//		activated = activated; //this actually does do something
//	}
//
//	// This interactable object has be interacted with
//	public override void OnInteract()
//	{
//		base.OnInteract ();
//
//		foreach(GameObject g in toggletargets)
//			g.SetActive (!g.activeSelf);
//
//		if(pairedConsole != null)
//			pairedConsole.activated = activated;
//		
//		foreach (Interactable i in subInteractables)
//			i.OnInteract ();
//	}
}
