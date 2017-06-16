using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuItem : MenuItem {

	public override void Activate()
	{
		base.Activate();
		GameController.instance.NextLevel();
		
	}
}
