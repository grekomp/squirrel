using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	public int score = 1;
	AudioSource audioSource;
	Animator animator;
	bool isActive = true;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && isActive)
		{
			isActive = false;
			GameController.instance.score += score;
			animator.SetTrigger("pickup");
			audioSource.Play();
			Destroy(gameObject, 0.7f);
		}
	}
}
