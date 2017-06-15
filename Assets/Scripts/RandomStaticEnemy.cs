using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStaticEnemy : MonoBehaviour {

	void Start () {
		int index = Random.Range(0, GameController.instance.staticEnemies.Length);
		GameObject.Instantiate(GameController.instance.staticEnemies[index], transform);
	}
}
