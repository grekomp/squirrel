using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipText : MonoBehaviour {

	Text skipText;

	Color targetColor;

	public Color activeColor;
	public Color inactiveColor;
	public bool active;
	public float transitionSpeed = 0.2f;

	void Start()
	{
		skipText = gameObject.GetComponent<Text>();

		if (active)
		{
			skipText.color = activeColor;
		}
		else
		{
			skipText.color = inactiveColor;
		}
	}

	void Update () {
		if (active)
		{
			skipText.color = Color.Lerp(skipText.color, activeColor, transitionSpeed);
		} else
		{
			skipText.color = Color.Lerp(skipText.color, inactiveColor, transitionSpeed);
		}
	}
}
