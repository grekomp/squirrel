using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public bool pausable = true;
	public bool restartable = true;

	void Start () {
		GameController.levelController = this;
	}

	void Update()
	{
		if (Input.GetButtonDown("Cancel")) TriggerPause();
		if (Input.GetButtonDown("Restart")) RestartLevel();
	}

	public void RestartLevel()
	{
		if (restartable)
		{
			GameController.RestartLevel();
		}
	}

	public void TriggerPause()
	{
		if (pausable)
		{
			GameController.instance.TriggerPause();
		}
	}
}
