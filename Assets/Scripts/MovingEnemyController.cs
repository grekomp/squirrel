using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyController : MonoBehaviour {

	public float animationStart;

	Animator animator;

	// Use this for initialization
	void Start ()
	{
		animator = gameObject.GetComponentInChildren<Animator>();

		animator.Play("Enemy Movement", -1, animationStart);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
