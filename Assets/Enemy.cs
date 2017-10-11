using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	//variables
	public Transform player;

	public float speed = 2f;
	private float sinTimer;
	private Rigidbody2D rigidBody;
	private float thrust;
	float timeSinceDamage = 1.0f;
	float timeSinceAttack = 0.0f;

	void Awake() {
		
	}

	void Start() {
		
		player = GameObject.Find("Player").transform;
		rigidBody = GetComponent<Rigidbody2D>();

	}

	void Update() {
		//enemy attack cooldown
		timeSinceAttack += Time.deltaTime;
		if (timeSinceDamage < 0.2f) {
			timeSinceDamage += Time.deltaTime;
		} else {
			GetComponentInParent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f);
		}
		//tracks player position
		Vector3 displacement = player.position - transform.position;
		displacement = displacement.normalized;
		//move towards the player
		if (Vector2.Distance (player.position, transform.position) < 10.0f) {
			//move if distance from player is greater than 7
			transform.position += (displacement * speed * Time.deltaTime);
		} else {
		}
	}
	// make enemy deal 10 damage each time collides with player.
	virtual public void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.SendMessage ("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
			Debug.Log ("enemy hit player");
			timeSinceAttack = 0.0f;
		}
	}
	//Deal another 10 damage every 0.6 seconds if the player keeps touching the enemy.
	virtual public void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player" && timeSinceAttack > 0.6f) {
			collision.gameObject.SendMessage ("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
			Debug.Log ("enemy hit player");
			timeSinceAttack = 0.0f;
		}
	}
	virtual public void DamageAnimation() {
		GetComponentInParent<SpriteRenderer> ().color = new Color (1.0f, 0.0f, 0.0f);
		timeSinceDamage = 0.0f;
	}
}