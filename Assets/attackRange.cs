using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackRange : MonoBehaviour {

	public float amount = 50;

	void OnTriggerEnter2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag ("Enemy")) 
		{
			col.SendMessageUpwards ("Damage", amount);
		}
		
	}
}
