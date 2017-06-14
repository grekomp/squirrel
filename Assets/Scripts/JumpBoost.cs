using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour {

	public int jumpCharges = 1;

	void Start () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<PlayerController>().jumpCharges += jumpCharges;
		}
	}
}
