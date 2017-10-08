using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCarTrigger : Trigger {
	DialogController dc;
	
	void Start() {
		dc = GameObject.Find ("DialogCanvas").GetComponent<DialogController> ();
	}
	public override void Enter() {
		Player player = GameController.GetPlayer ();
		if (Player.HasItem (Item.LEVER)) {
			player.Teleport (-17.0f, 110.0f);
			dc.StartDialog (new DialogLoader ().LoadJson ("Area2Intro"));
		} else {
			dc.StartDialog (new DialogLoader ().LoadJson ("HandCar1"));
		}

	}
}
