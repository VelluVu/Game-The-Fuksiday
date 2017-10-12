using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKim : Enemy {



	// On Collision with player send message to player which then takes damage.
	public override void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Player")
			collision.gameObject.SendMessage ("TakeDamage", 25, SendMessageOptions.DontRequireReceiver);
		Debug.Log ("KIM hits player");
	}
}