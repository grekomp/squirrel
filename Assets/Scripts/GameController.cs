using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController instance;

	public static LevelController levelController;

	void Awake () {
		// Enforce singleton pattern
		if (instance == null)
		{
			instance = this;
		} else
		{
			Destroy(gameObject);
		}

		// Don't destroy game controller when a new level is loaded 
		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		if (Input.GetButtonDown("Restart")) RestartLevel();
	}

	void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
