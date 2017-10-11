using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKimHealth : EnemyHealth {

	public AudioClip enemyPainSound;

	private AudioSource source4;


	public override void EnemyTakeDamage(float amount)
	{
		source4.PlayOneShot (enemyPainSound, 1F);
		enemyHealth -= amount;
		if (enemyHealth <= 0) {
			Destroy (gameObject);
		}
	}
	void Awake() {
		source4 = GetComponent<AudioSource> ();
}
}