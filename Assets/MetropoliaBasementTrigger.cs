using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetropoliaBasementTrigger : Trigger {
	DialogController dc;

	void Start() {
		dc = GameObject.Find ("DialogCanvas").GetComponent<DialogController> ();
	}
	public override void Enter() {
		Player player = GameController.GetPlayer ();
		if (Player.HasItem (Item.GUNPOWDER) && Player.HasItem (Item.JESUSTAPE) && Player.HasItem (Item.TOILETPAPERROLL)) {
			player.Teleport (-51.59f, 269.74f);
			dc.StartDialog (new DialogLoader ().LoadJson ("Area3Basement1"));
		} else {
			dc.StartDialog (new DialogLoader ().LoadJson ("Area3Basement2"));
		}

	}
}
