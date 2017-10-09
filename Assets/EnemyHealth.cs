using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	
	public AudioClip meowsInPain;

	private AudioSource source3;

	public float enemyHealth = 100;

	public void EnemyTakeDamage(float amount)
	{
		source3.PlayOneShot (meowsInPain, 1F);
		enemyHealth -= amount;
		if (enemyHealth <= 0) {
			Destroy (gameObject);
		}
	}
	public void Damage (float amount){
	}
	void Awake() {
		source3 = GetComponent<AudioSource> ();
	}
}