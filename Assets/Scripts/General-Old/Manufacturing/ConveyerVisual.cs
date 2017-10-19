using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConveyerVisual : MonoBehaviour 
{	
	private ConveyerBelt_v2 linkedConveyer;
	private ConveyerController controller;
	private SpriteRenderer sprite;
	void Awake()
	{
		linkedConveyer = transform.parent.GetComponent<ConveyerBelt_v2> ();
		controller = transform.GetComponentInParent<ConveyerController> ();
		sprite = transform.GetComponentInChildren<SpriteRenderer> ();
	}
	// Update is called once per frame
	void Update () 
	{
		linkedConveyer = transform.GetComponentInParent<ConveyerBelt_v2> ();
		controller = transform.GetComponentInParent<ConveyerController> ();
		sprite = transform.GetComponentInChildren<SpriteRenderer> ();
		if (linkedConveyer == null || controller == null)
			return;
		transform.localPosition = Vector3.zero;
		if(!controller.isReversed) 
		{
			switch(linkedConveyer.positiveDirection)
			{
			case ConveyerBelt_v2.Directions.UP:
				transform.eulerAngles = new Vector3 (0, 0, 90);
				break;
			case ConveyerBelt_v2.Directions.RIGHT:
				transform.eulerAngles = new Vector3 (0, 0, 0);
				break;
			case ConveyerBelt_v2.Directions.DOWN:
				transform.eulerAngles = new Vector3 (0, 0, -90);
				break;
			case ConveyerBelt_v2.Directions.LEFT:
				transform.eulerAngles = new Vector3 (0, 0, 180);
				break;
			}
		}
		else
		{
			switch(linkedConveyer.reverseDirection)
			{
			case ConveyerBelt_v2.Directions.UP:
				transform.eulerAngles = new Vector3 (0, 0, 90);
				break;
			case ConveyerBelt_v2.Directions.RIGHT:
				transform.eulerAngles = new Vector3 (0, 0, 0);
				break;
			case ConveyerBelt_v2.Directions.DOWN:
				transform.eulerAngles = new Vector3 (0, 0, -90);
				break;
			case ConveyerBelt_v2.Directions.LEFT:
				transform.eulerAngles = new Vector3 (0, 0, 180);
				break;
			}
		}
		transform.localScale = new Vector3 (1, 1, 1);
		if (controller.isOn)
			sprite.color = Color.yellow;
		else
			sprite.color = Color.white;
	}
}
