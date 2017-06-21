using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Vector3 followOffset;
	public float followSpeed = 0.1f;
	public GameObject target;

	public static Vector3 lastPosition;
	public static Vector3 currentPosition;
	public static Vector3 cameraMovement;

	void Start () {
		// If no target is specified, follow the player
		if (target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player");
		}

		// Set starting position
		transform.position = target.transform.position + followOffset;

		// Set last and current position
		lastPosition = transform.position;
		currentPosition = transform.position;
	}
	
	void Update () {
		// Update last position
		lastPosition = transform.position;

		// Update position by smoothly following the player
		transform.position = Vector3.Lerp(transform.position, target.transform.position + followOffset, followSpeed * Time.deltaTime);

		// Update current position and movement
		currentPosition = transform.position;
		cameraMovement = currentPosition - lastPosition;
	}
}
