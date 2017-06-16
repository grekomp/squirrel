using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicController : MonoBehaviour {

	Animator[] frames;
	public int currentFrame;

	public float frameTime = 5.0f;
	public float remainingFrameTime;

	public float maxSkipDelay = 3.0f;
	float skipDelay;
	SkipText skipText;

	void Start()
	{
		skipText = GameObject.Find("Skip Text").GetComponent<SkipText>();

		frames = gameObject.GetComponentsInChildren<Animator>();

		frames[0].SetTrigger("in");

		remainingFrameTime = frameTime;
	}

	void Update()
	{
		// Switching frames
		remainingFrameTime -= Time.deltaTime;

		if (remainingFrameTime <= 0)
		{
			if (currentFrame < frames.Length - 1)
			{
				frames[currentFrame].SetTrigger("out");
				currentFrame++;
				frames[currentFrame].SetTrigger("in");

				remainingFrameTime = frameTime;
			}
			else
			{
				GameController.instance.NextLevel();
			}
		}

		// Skipping comic
		if (skipDelay > 0)
		{
			if (Input.GetKeyDown("return"))
			{
				GameController.instance.NextLevel();
			}

			skipDelay -= Time.deltaTime;

			skipText.active = true;
		} else
		{
			skipText.active = false;
		}

		if (Input.anyKeyDown)
		{
			skipDelay = maxSkipDelay;
		}
	}
}
