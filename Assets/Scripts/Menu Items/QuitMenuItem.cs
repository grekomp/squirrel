using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenuItem : MenuItem {

	public override void Activate()
	{
		base.Activate();
		GameController.Quit();
		
	}
}
