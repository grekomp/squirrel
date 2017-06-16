using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public Vector3 targetPosition;
	public float transitionSpeed = 0.2f;


	void Start () {
		targetPosition = transform.position;
	}
	
	void Update () {
		transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
		
	}
}
