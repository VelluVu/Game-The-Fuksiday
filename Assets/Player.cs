﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	float playerMaxSpeed = 5.0f;
	float acceleration = 12.0f;
	float deceleration = 12.0f;

	public float playerSpeedX = 0f;
	public float playerSpeedY = 0f;



	GameObject healthBar;
	Button strike;
	GameObject oilCan;
	GameObject wrench;
	GameObject lever;
	GameObject sword;
	GameObject player;
	GameObject jumpsuit;
	GameObject greenGoo;
	GameObject gunPowder;
	GameObject jesusTape;
	GameObject toiletPaperRoll;
	static List<Item> inventory;
	Health health;

	private bool alive = true;
	private bool attack;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private Animator anim;

	public Collider2D attackRange;
	// Use this for initialization
	void awake ()
	{
		
	}

	void Start ()
	{

		inventory = new List <Item> ();
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player");
		strike = GameObject.Find ("ButtonMelee").GetComponent<Button> ();
		health = GetComponentInParent<Health> ();
		healthBar = GameObject.Find ("HealthBar");
		strike.onClick.AddListener (HandleInput);
		oilCan = GameObject.Find ("OilCan");
		wrench = GameObject.Find ("Wrench");
		lever = GameObject.Find ("Lever");
		oilCan = GameObject.Find ("Jumpsuit");
		greenGoo = GameObject.Find ("GreenGoo");
		gunPowder = GameObject.Find ("GunPowder");
		jesusTape = GameObject.Find ("JesusTape");
		toiletPaperRoll = GameObject.Find ("ToiletPaperRoll");
		sword = GameObject.Find ("Sword");
		anim = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		attackRange.enabled = false;
	}

	public Health GetHealth ()
	{
		return health;
	}

	public void Die() {
		GameController.PlayerDied ();
		alive = false;
	}

	public bool IsAlive() {
		return alive;
	}

	void Update ()
	{
		//HandleInput();
		healthBar.GetComponent<RectTransform> ().anchorMax = (new Vector2 (health.health / 100.0f, 1.0f));
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		HandleMovement ();

		anim.SetFloat ("Speed", Mathf.Abs (playerSpeedX+ playerSpeedY));

		HandleAttacks ();

		ResetValues ();
	}



	public void AddToInventory (Item item)
	{
		Debug.Log ("Collected " + item.GetVisibleName ());
		inventory.Add (item);
		if (Player.HasItem (Item.JUMPSUIT)) {
			greenGoo.GetComponent<PolygonCollider2D> ().enabled = false;
			GameObject.Find ("DialogTrigger goo1").GetComponent<DialogTrigger> ().TriggerCount = 0;
		}
		GameController.ShowText ("Collected " + item.GetVisibleName ());
	}

	public static void RemoveFromInventory (Item item)
	{
		inventory.Remove (item);
	}

	public static List<Item> GetInventory ()
	{
		return inventory;
	}

	public void Teleport (float x, float y)
	{
		this.transform.position = new Vector3 (x, y, 0.0f);
	}

	public static bool HasItem (int id)
	{
		bool item = false;
		foreach (Item invitem in inventory) {
			if (invitem.GetId () == id) {
				item = true;
			}
		}
		return item;
	}

	//Sets Object with tag "Pick Up" unactive when collide.
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			//Add item to list inventory
			AddToInventory (other.gameObject.GetComponent<Item> ());
			other.gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("WorldPortal")) {
			WorldPortal portal = other.gameObject.GetComponent<WorldPortal> ();
			if (portal.isEnabled ()) {
				this.transform.position = new Vector3 ((float)portal.getX (), (float)portal.getY (), 0.0f);
			}
		}
		if (other.gameObject.CompareTag ("Trigger")) {
			Trigger t = other.gameObject.GetComponent<Trigger> ();
			t.Enter ();
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Trigger")) {
			Trigger t = other.gameObject.GetComponent<Trigger> ();
			t.Stay ();
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Trigger")) {
			Trigger t = other.gameObject.GetComponent<Trigger> ();
			t.Exit ();
		}
	}


	public void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
			Debug.Log ("I collide the enemy");
	}


	private void HandleAttacks ()
	{
		if (attack) {
			anim.SetTrigger ("Attack");
		}
	}


	private void HandleInput ()
	{
		if (Player.HasItem (Item.SWORD)) {
			attack = true;
			Debug.Log ("You swing with sword");
		} else {
			attack = false;
			Debug.Log ("You have no weapon");
		}
	}

	private void HandleMovement () {
		//flip player sprite
		if (spriteRenderer != null) {
			if (Input.GetPressed (Input.LEFT)) {
				spriteRenderer.flipX = true;
			}
			if (Input.GetPressed (Input.RIGHT)) {
				spriteRenderer.flipX = false;

			}
		}
		if ((Input.GetPressed (Input.LEFT) || Input.GetPressed (Input.RIGHT)) && (Input.GetPressed (Input.DOWN) || Input.GetPressed (Input.UP))) {
			playerMaxSpeed = 5.0f/Mathf.Sqrt (2.0f);
		} else {
			playerMaxSpeed = 5.0f;
		}
		//Easy movement need to change into method and call it in GameControl
		if (Input.GetPressed (Input.LEFT)) {
			if (playerSpeedX >= -playerMaxSpeed) {
				playerSpeedX -= (acceleration * Time.deltaTime);
				//player.transform.Translate (-playerSpeed, 0, 0);
			}
		}
		if (Input.GetPressed (Input.RIGHT)) {
			if (playerSpeedX <= playerMaxSpeed) {
				playerSpeedX += (acceleration * Time.deltaTime);
				//	player.transform.Translate (playerSpeed, 0, 0);
			}
		}
		if (Input.GetPressed (Input.UP)) {
			if (playerSpeedY <= playerMaxSpeed) {
				playerSpeedY += (acceleration * Time.deltaTime);
				//player.transform.Translate (0, playerSpeed, 0);
			}
		}
		if (Input.GetPressed (Input.DOWN)) {
			if (playerSpeedY >= -playerMaxSpeed) {
				playerSpeedY -= (acceleration * Time.deltaTime); 
			}
		}

		if ((!Input.GetPressed (Input.LEFT) && !Input.GetPressed (Input.RIGHT) || Mathf.Abs(playerSpeedX) > playerMaxSpeed) && playerSpeedX != 0.0) {
			if (playerSpeedX > 0) {
				playerSpeedX = Mathf.Max (0, playerSpeedX - (deceleration * Time.deltaTime));
			} else {
				playerSpeedX = Mathf.Min (0, playerSpeedX + (deceleration * Time.deltaTime));
			}
		}
		if ((!Input.GetPressed (Input.DOWN) && !Input.GetPressed (Input.UP) || Mathf.Abs(playerSpeedY) > playerMaxSpeed) && playerSpeedY != 0.0) {
			if (playerSpeedY > 0) {
				playerSpeedY = Mathf.Max (0, playerSpeedY - (deceleration * Time.deltaTime));
			} else {
				playerSpeedY = Mathf.Min (0, playerSpeedY + (deceleration * Time.deltaTime));
			}
		}
		//Debug.Log (deceleration * Time.deltaTime * ((playerSpeedY > 0) ? -1.0f : 1.0f));
		player.transform.Translate (playerSpeedX * Time.deltaTime, playerSpeedY * Time.deltaTime, 0);
	}

	private void ResetValues () {
		attack = false;
	}
}