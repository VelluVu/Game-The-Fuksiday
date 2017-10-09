using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	public int dmg;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag ("Player")) {
			
			other.SendMessage ("EnemyTakeDamage", dmg, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}
	void FixedUpdate () {
		
	}
}
