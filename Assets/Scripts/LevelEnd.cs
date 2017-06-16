using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (other.transform.position.x > transform.position.x)
			{
				Debug.Log("Flipping");
				transform.localScale = new Vector3(-1, 1, 1);
			}

			GetComponentInChildren<Animator>().SetTrigger("Level End");
			other.GetComponent<PlayerController>().EndLevel();
		}
	}
}
