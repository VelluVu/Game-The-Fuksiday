using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiCannon : MonoBehaviour {

	public Transform target;
	public float attackCooldown;
	private float timeSinceShoot;
	public float attackRange;
	public float cannonPower;
	public GameObject sushiRoll;

	void Start (){
		target = GameObject.Find("Player").transform;
	}


	void Update () {
		//Attacking AI Ranged

		//Check to see if the player is within our attack range
		float distanceToPlayer = Vector3.Distance(transform.position, target.position);
		if (distanceToPlayer < attackRange) {

			//turn towards the player
			Vector3 targetDir = target.position - transform.position;
			float angle = Mathf.Atan2 (targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 90 * Time.deltaTime);

			//Check to see if it's time to attack
			if (Time.time > timeSinceShoot * attackCooldown) {

				//Raycast to see if kim can throw object at the player
				RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, attackRange);

				//Check to see if we hit anything and what it was
				if (hit.transform == target) {

					//hit the player - fire the projectile
					GameObject newSushiRoll = Instantiate (sushiRoll, transform.position, transform.rotation);
					newSushiRoll.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (0f,cannonPower));
					timeSinceShoot = Time.time;

				}
			}
		}
	}
}
