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
	public int deathCount = 0;

	public static bool playerHasControl = true;
	public static bool paused = false;

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
	}

	public static void RestartLevel()
	{
		instance.score = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void NextLevel()
	{
		score = 0;
		currentLevel++;
		SceneManager.LoadScene(levels[currentLevel].scene);
	}

	public Sprite GetBackground()
	{
		return backgrounds[levels[currentLevel].backgroundIndex];
	}

	public void TriggerPause()
	{
		paused = !paused;

		Pause(paused);
	}

	public static void Quit()
	{
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

	public void LoadLevel (int index)
	{
		score = 0;
		currentLevel = index;
		Pause(false);
		SceneManager.LoadScene(levels[currentLevel].scene);
	}

	public void Pause(bool pause)
	{
		paused = pause;

		if (paused)
		{
			playerHasControl = false;
			Time.timeScale = 0.0f;
		}
		else
		{
			Time.timeScale = 1.0f;
		}
	}

}
