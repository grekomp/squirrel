using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour {

	Vector3 initialPosition;
	float depth;

	static float maxDepth = 20f;
	static float movementMultiplier = 0.5f;

	void Start () {
		// Set Initial position
		initialPosition = transform.position;
		depth = transform.localPosition.z;
	}
	
	void LateUpdate () {
		// Calculate movement based on camera movement and height
		transform.position =  initialPosition - (transform.position - CameraController.currentPosition) * depth / maxDepth * movementMultiplier;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, depth);
	}
}
