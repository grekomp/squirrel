using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController instance;
	public static LevelController levelController;

	public GameObject[] staticEnemies;
	public Level[] levels;
	public Sprite[] backgrounds;

	public static int currentLevel;
	public int score = 0;

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

	public static void RestartLevel()
	{
		instance.score = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void NextLevel()
	{
		currentLevel++;
		SceneManager.LoadScene(levels[currentLevel].scene);

	}

	public Sprite GetBackground()
	{
		return backgrounds[levels[currentLevel].backgroundIndex];
	}
}
