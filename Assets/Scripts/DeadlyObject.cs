using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObject : MonoBehaviour {

	void Start () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag("Player"))
		{
			other.GetComponent<PlayerController>().Death(other.transform.position - transform.position);
		}

	}
}
