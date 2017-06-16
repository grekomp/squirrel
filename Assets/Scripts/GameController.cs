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
	public AudioClip[] musicTracks;

	public static int currentLevel;
	public int score = 0;
	public int deathCount = 0;
	public float nextLevelDelay = 2.0f;

	public static bool playerHasControl = true;
	public static bool paused = false;

	AudioSource audioSource;

	void Awake () {
		// Enforce singleton pattern
		if (instance == null)
		{
			instance = this;

			audioSource = GetComponent<AudioSource>();
			audioSource.clip = musicTracks[0];
			audioSource.Play();
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
		//instance.score = 0;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		instance.LoadLevel(currentLevel);
	}

	public void NextLevel()
	{
		LoadLevel(currentLevel + 1);
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
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	public void LoadLevel (int index)
	{
		// Reset Score
		score = 0;

		// If loading level with different music, change music
		if (levels[index].musicIndex != levels[currentLevel].musicIndex)
		{
			audioSource.clip = musicTracks[levels[index].musicIndex];
			audioSource.Stop();
			audioSource.Play();
		}

		// Change current level
		currentLevel = index;

		// Unpause game
		Pause(false);

		// Load Scene
		SceneManager.LoadScene(levels[currentLevel].scene);
	}

	public void Pause(bool pause)
	{
		paused = pause;

		if (paused)
		{
			playerHasControl = false;
			Time.timeScale = 0.0f;
			audioSource.Pause();
		}
		else
		{
			Time.timeScale = 1.0f;
			if (!audioSource.isPlaying)
				audioSource.Play();
		}
	}

	public void EndLevel()
	{
		Invoke("NextLevel", nextLevelDelay);
	}

}
