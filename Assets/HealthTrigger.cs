using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTrigger : Trigger {

	public float initialDamage = 10;
	public float damagePerSecond = 10;
	public float exitDamage = 5;
	public override void Enter () {
		GameController.GetPlayer ().GetHealth ().TakeDamage(initialDamage);
	}
	
	// Update is called once per frame
	public override void Stay () {
		GameController.GetPlayer ().GetHealth ().TakeDamage(damagePerSecond * Time.deltaTime);
	}

	public override void Exit () {
		GameController.GetPlayer ().GetHealth ().TakeDamage(exitDamage);
	}
}
