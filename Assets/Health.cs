using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

	public float health = 100;

	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0 && GameController.GetPlayer().IsAlive()) {
			GameController.GetPlayer().Die ();
		}
	}
	public void Heal(float amount) {
		health += amount;
	}
}
