using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public class Sushiroll : MonoBehaviour {

		public int dmg;

		void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag ("Enemy")) {

				other.SendMessage ("TakeDamage", dmg, SendMessageOptions.DontRequireReceiver);
			}
		}
		void FixedUpdate () {


	}
}
