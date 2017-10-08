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
		if (Player.HasItem (Item.GUNPOWDER)) {
			player.Teleport (-51.59f, 269.74f);
			dc.StartDialog (new DialogLoader ().LoadJson (""));
		} else {
			dc.StartDialog (new DialogLoader ().LoadJson (""));
		}

	}
}
