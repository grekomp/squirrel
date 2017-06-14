using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreset: MonoBehaviour {
	// Movement
	public float moveSpeed = 4.0f;
	public float acceleration = 2.0f;
	public float breaking = 4.0f;
	public float airBreaking = 2.0f;

	// Jumping
	public float jumpHeight = 5.0f;
	public float jumpingGravityScale = 1.0f;
	public int jumpCharges = 0;
}
