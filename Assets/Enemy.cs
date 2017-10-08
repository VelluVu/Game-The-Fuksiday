using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Transform Player;
	private float speed = 2f;
	//private float attack = 10f;
	Enemy mutantCat;
	private float sinTimer;
	private Rigidbody2D rigidBody;
	private float thrust;


	void Start() {
		Player = GameObject.Find ("Player").transform;
		rigidBody = GetComponent<Rigidbody2D>();
	}
	void Update() {
		Vector3 displacement = Player.position - transform.position;
		displacement = displacement.normalized;
		//move towards the player
		if (Vector2.Distance (Player.position, transform.position) < 7.0f) {
			//move if distance from player is greater than 7
			transform.position += (displacement * speed * Time.deltaTime);
		} else {
		}
	}
	// Trying to make enemy deal 10 damage each time collides with player.
	public void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Player")
			Debug.Log ("enemy hit player");



		

		
	}
}