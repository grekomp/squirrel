using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Vector3 followOffset;
	public float followSpeed = 0.1f;
	public GameObject target;

	void Start () {
		transform.position = target.transform.position + followOffset;
	}
	
	void Update () {
		transform.position = Vector3.Lerp(transform.position, target.transform.position + followOffset, followSpeed * Time.deltaTime);
	}
}
