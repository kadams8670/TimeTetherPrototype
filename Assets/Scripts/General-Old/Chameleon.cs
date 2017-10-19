using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : MonoBehaviour {

	public bool isHidden;
	public bool isInHideZone;
	public KeyCode hideButton;
	public float rechargeRate;
	public float useTime;
	public float charge;

	// Use this for initialization
	void Start () 
	{
		charge = useTime;
	}

	// Update is called once per frame
	void Update () 
	{
		if(isInHideZone && Input.GetKeyDown(hideButton))
		{
			ToggleHide ();
		}
		if(isHidden && !isInHideZone)
		{
			ToggleHide ();
		}

		if(isHidden)
		{
			charge -= Time.deltaTime;
			if (charge <= 0)
				ToggleHide ();
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.5f - charge / useTime, 0.5f + (charge / useTime * 0.112f), 0.5f - (charge / useTime * 0.3f));
		}
		else 
		{
			if (charge <= useTime)
				charge += Time.deltaTime * rechargeRate;
			else
				charge = useTime;
			if (charge >= useTime - 0.1f && charge < useTime)
				gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
			else
			{
				gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
			}
		} 
	}

	void ToggleHide()
	{
		isHidden = !isHidden;
		if(isHidden)
		{
			gameObject.layer = 14;

		}
		else
		{
			gameObject.layer = 13;
		}
	}
}
