using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public AudioClip[] enemySounds;
	AudioSource audioSource;
	public float minSoundDelay = 3.0f;
	public float maxSoundDelay = 6.0f;
	float soundDelay;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		soundDelay = Random.Range(minSoundDelay, maxSoundDelay);
	}
	
	void Update () {
		soundDelay -= Time.deltaTime;

		if (soundDelay <= 0)
		{
			PlayRandomSound();
			soundDelay = Random.Range(minSoundDelay, maxSoundDelay);
		}

		
	}

	void PlayRandomSound()
	{
		audioSource.clip = enemySounds[Random.Range(0, enemySounds.Length)];
		audioSource.Play();
	}

	
}
