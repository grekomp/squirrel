using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	Sprite background;
	SpriteRenderer spriteRenderer;

	void Start () {
		background = GameController.instance.GetBackground();

		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = background;

		float screenAspectRatio = Screen.width / Screen.height;
		float backgroundAspectRatio = spriteRenderer.bounds.extents.x / spriteRenderer.bounds.extents.y;

		if (screenAspectRatio > backgroundAspectRatio)
		{
			float scale = screenAspectRatio / backgroundAspectRatio;
			transform.localScale = new Vector3(scale, scale, 1);
		}
	}
}
