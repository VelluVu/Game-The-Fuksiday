using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

	public float health = 100;

	public AudioClip pain;

	private AudioSource source;

	public void TakeDamage(float amount)
	{
		source.PlayOneShot (pain, 1F);
		health -= amount;
		if (health <= 0 && GameController.GetPlayer().IsAlive()) {
			GameController.GetPlayer().Die ();
		}
	}
	public void Heal(float amount) {
		health += amount;
	}
	void Awake () {
	source = GetComponent<AudioSource> ();
}
}
