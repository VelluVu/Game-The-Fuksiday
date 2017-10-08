using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float enemyHealth = 100;

	public void TakeDamage(float amount)
	{
		enemyHealth -= amount;
		if (enemyHealth <= 0) {
			Destroy (gameObject);
		}
	}
	public void Damage (float amount){
	}
}