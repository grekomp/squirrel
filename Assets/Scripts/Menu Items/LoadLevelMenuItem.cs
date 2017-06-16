using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelMenuItem : MenuItem {

	public int levelIndex;

	public override void Activate()
	{
		base.Activate();

		GameController.instance.LoadLevel(levelIndex);
	}
}
