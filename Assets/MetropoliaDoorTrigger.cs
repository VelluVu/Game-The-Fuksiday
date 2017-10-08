using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetropoliaDoorTrigger : Trigger {
	DialogController dc;

	void Start() {
		dc = GameObject.Find ("DialogCanvas").GetComponent<DialogController> ();
	}
	public override void Enter() {
		Player player = GameController.GetPlayer ();
		if (Player.HasItem (Item.JUMPSUIT)) {
			player.Teleport (-65.02f, 175.22f);
			dc.StartDialog (new DialogLoader ().LoadJson (""));
		} else {
			dc.StartDialog (new DialogLoader ().LoadJson (""));
		}

	}
}
