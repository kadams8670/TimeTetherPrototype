using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	/* Static Vars */

	/* Instance Vars */

	// The Transform of the GameObject to which the camera is attached
	private Transform cam;

	[SerializeField]
	public int playerNumber = 1;

	// Target stuff
	public Transform target;
	public bool isFollowingTarget;

	// Shake stuff
	private float shakeDur = 0f;
	private float shakeIntensity;
	private float shakeDecay;

	// Misc
	[SerializeField]
	private float lookAheadMagnitude = 5f;

	/* Instance Methods */
	public void Awake()
	{
		cam = transform.GetChild (0);
	}

	public void Update()
	{
		//look ahead
		Vector3 lookAheadPos = new Vector3 (Mathf.Round(Input.GetAxis ("RightH" + playerNumber)), Mathf.Round(Input.GetAxis ("RightV" + playerNumber)), 0f) * lookAheadMagnitude;
		cam.localPosition = Vector3.Lerp (cam.localPosition, lookAheadPos, Time.deltaTime * lookAheadMagnitude);

		//shake the camera
		if (shakeDur > 0f)
		{
			cam.localPosition += (Vector3)Random.insideUnitCircle * shakeIntensity;

			shakeIntensity -= shakeDecay * Time.deltaTime;
			if (shakeIntensity <= 0f)
				shakeDur = 0f;

			shakeDur -= Time.deltaTime;
			if (shakeDur <= 0f)
			{
				shakeDur = 0f;
				shakeIntensity = 0f;
				shakeDecay = 0f;

				//cam.localPosition = Vector2.zero;
			}
		}
	}

	public void LateUpdate()
	{
		//stay locked to the target's position
		if (isFollowingTarget && target != null)
		{
			transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
		}
	}

	// Assigns the target and toggles target following
	public void setTarget(Transform target)
	{
		this.target = target;
		isFollowingTarget = true;
	}

	// Unassigns the target and frees the camera
	public void freeCamera()
	{
		target = null;
		isFollowingTarget = false;
	}

	// Shakes the camera for some a duration
	public void shakeCamera(float duration, float intensity, float decayRate = 0f)
	{
		shakeDur = duration;
		shakeIntensity = intensity;
		shakeDecay = decayRate;
	}
}
