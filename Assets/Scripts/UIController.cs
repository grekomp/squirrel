using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	// Pause
	GameObject pauseMenu;
	bool paused;

	// Score
	Text scoreText;
	Text deathText;

	// Level Intro Text
	Text levelIntroText;

	void Start () {
		pauseMenu = GameObject.Find("Pause Menu");
		pauseMenu.SetActive(paused);

		scoreText = GameObject.Find("Score Text").GetComponent<Text>();
		deathText = GameObject.Find("Death Text").GetComponent<Text>();
		levelIntroText = GameObject.Find("Level Intro Text").GetComponent<Text>();

		levelIntroText.text = SceneManager.GetActiveScene().name;
	}
	
	void Update () {
		// Check if game paused status changed
		if (GameController.paused != paused)
		{
			paused = GameController.paused;
			pauseMenu.SetActive(paused);
		}

		// Update Score
		scoreText.text = GameController.instance.score.ToString();
		deathText.text = GameController.instance.deathCount.ToString();
	}
}
