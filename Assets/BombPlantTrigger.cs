using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlantTrigger : Trigger {
	

	public override void Enter () {
		if (enemiesAreDead()) {
			DialogController dc = GameObject.Find ("DialogCanvas").GetComponent<DialogController> ();
			dc.StartDialog (new DialogLoader ().LoadJson ("Area4Bomb"), 0);

		}

	}
	bool enemiesAreDead() {
		//Have both Kim Jong Un and the other boss enemy been destroyed?
		return ((GameObject.Find ("KIMyonBoss") == null) && (GameObject.Find ("MutatedCreature Big")== null));
	}
}
