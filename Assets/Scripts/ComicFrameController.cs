using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicFrameController : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		Camera cam = Camera.main;

		float backgroundAspectRatio = spriteRenderer.bounds.extents.x / spriteRenderer.bounds.extents.y;

		if (cam.aspect < backgroundAspectRatio)
		{
			float scale = cam.aspect / backgroundAspectRatio;
			transform.localScale = new Vector3(scale, scale, 1);
		}
	}
}
