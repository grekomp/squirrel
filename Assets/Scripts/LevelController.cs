using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	
	void Start () {
		GameController.levelController = this;
	}

	public void RestartLevel()
	{
		GameController.RestartLevel();
	}
}
