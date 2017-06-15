using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Vector3 followOffset;
	public float followSpeed = 0.1f;
	public GameObject target;

	void Start () {
		// If no target is specified, follow the player
		if (target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player");
		}

		// Set starting position
		transform.position = target.transform.position + followOffset;
	}
	
	void Update () {
		// Update position by smoothly following the player
		transform.position = Vector3.Lerp(transform.position, target.transform.position + followOffset, followSpeed * Time.deltaTime);
	}
}
