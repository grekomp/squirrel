using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	// Pause
	GameObject pauseMenu;
	bool paused;

	// Score
	Text scoreText;
	Text deathText;

	void Start () {
		pauseMenu = GameObject.Find("Pause Menu");
		pauseMenu.SetActive(paused);

		scoreText = GameObject.Find("Score Text").GetComponent<Text>();
		deathText = GameObject.Find("Death Text").GetComponent<Text>();
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
