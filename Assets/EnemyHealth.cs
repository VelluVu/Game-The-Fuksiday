using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	
	public AudioClip meowsInPain;

	private AudioSource source3;

	public float enemyHealth;
	//enemy health and damages it + plays sound.
	public virtual void EnemyTakeDamage(float amount)
	{
		GetComponentInParent<Enemy> ().DamageAnimation ();
		source3.PlayOneShot (meowsInPain, 1F);
		enemyHealth -= amount;
		if (enemyHealth <= 0) {
			//destroy gameobject when health reach 0 or less
			Destroy (gameObject);
		}
	}
	public void Damage (float amount){
	}
	void Awake() {
		source3 = GetComponent<AudioSource> ();
	}
}