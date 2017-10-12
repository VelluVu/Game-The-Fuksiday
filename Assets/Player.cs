using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	//movement variables
	float playerMaxSpeed = 5.0f;
	float acceleration = 12.0f;
	float deceleration = 12.0f;

	public float playerSpeedX = 0f;
	public float playerSpeedY = 0f;
	//
	float timeSinceAttack = 10.0f;

	//melee variables
	public float meleeRange;
	public int meleeDamage;

	//audio
	public AudioClip LootSound;
	public AudioClip shootSound;
	public AudioClip swordSound;
	public AudioClip walkingSound;
	private AudioSource source;

	//bools for directions
	public bool directionL = false;
	public bool directionR = false;
	public bool directionU = false;
	public bool directionD = false;

	//objects and buttons
	GameObject healthBar;
	Button strike;
	Button shoot;
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
	GameObject bow;

	//arrows...
	public GameObject arrowObjDown;
	public GameObject arrowObjUp;
	public GameObject arrowObjRight;
	public GameObject arrowObjLeft;
	//inventory list
	static List<Item> inventory;
	//health
	Health health;

	//bools and unity tools
	public string direction = "left";
	private bool alive = true;
	private bool attack;
	private bool rangedAttack;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private Animator anim;

	// Use this for initialization
	void awake ()
	{
		
	}

	void Start ()
	{
		//lots of objects and components
		source = GetComponent<AudioSource> ();
		shoot = GameObject.Find ("ButtonRanged").GetComponent<Button> ();
		shoot.onClick.AddListener (HandleRanged);
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
		bow = GameObject.Find ("MirkWoodBow");
		toiletPaperRoll = GameObject.Find ("ToiletPaperRoll");
		sword = GameObject.Find ("Sword");
		anim = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		//image of shoot and melee buttons
		shoot.GetComponentInParent<Image> ().enabled = false;
		strike.GetComponentInParent<Image> ().enabled = false;
	}
	public Health GetHealth ()
	{
		return health;
	}
	//player is dead if not alive??!
	public void Die() {
		GameController.PlayerDied ();
		alive = false;
	}
	//checks if player is alive
	public bool IsAlive() {
		return alive;
	}

	void Update ()
	{
		//cooldown counter
		timeSinceAttack += Time.deltaTime;
		//HandleInput();
		//updates the players health bar
		healthBar.GetComponent<RectTransform> ().anchorMax = (new Vector2 (health.health / 100.0f, 1.0f));
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		//calls the move method
		HandleMovement ();
		//walk move animation
		anim.SetFloat ("Speed", Mathf.Sqrt (Mathf.Pow (playerSpeedX, 2) + Mathf.Pow (playerSpeedY, 2)));
		//calls the attack method
		HandleAttacks ();
		//resets attack and bow shoot
		ResetValues ();
	}


	//adds item to inventory
	public void AddToInventory (Item item)
	{
		Debug.Log ("Collected " + item.GetVisibleName ());
		inventory.Add (item);
		//checks if player has item jumpsuit if has makes damaging pool in area 2 unactive
		if (Player.HasItem (Item.JUMPSUIT)) {
			greenGoo.GetComponent<PolygonCollider2D> ().enabled = false;
			GameObject.Find ("DialogTrigger goo1").GetComponent<DialogTrigger> ().TriggerCount = 0;
		}
		//shows sword attack image when have sword in inventory
		if (Player.HasItem (Item.SWORD)) {
			strike.GetComponentInParent<Image> ().enabled = true;
		}
		//shows bow attack image when have bow in inventory
		if (Player.HasItem (Item.BOW)) {
			shoot.GetComponentInParent<Image> ().enabled = true;
		}
		//shows text of the item
		GameController.ShowText ("Collected " + item.GetVisibleName ());
	}
	//removes item from inventory
	public static void RemoveFromInventory (Item item)
	{
		inventory.Remove (item);
	}
	//finds inventory
	public static List<Item> GetInventory ()
	{
		return inventory;
	}

	public void Teleport (float x, float y)
	{
		//teleports player to this position
		this.transform.position = new Vector3 (x, y, 0.0f);
	}

	public static bool HasItem (int id)
	{
		//checks through item inventory if player has certain item with ID returns that item. 
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
			source.PlayOneShot (LootSound, 1F);
			//Add item to list inventory
			AddToInventory (other.gameObject.GetComponent<Item> ());
			other.gameObject.SetActive (false);
		}
		//if world portal is enabled allows player to move through it
		if (other.gameObject.CompareTag ("WorldPortal")) {
			WorldPortal portal = other.gameObject.GetComponent<WorldPortal> ();
			if (portal.isEnabled ()) {
				this.transform.position = new Vector3 ((float)portal.getX (), (float)portal.getY (), 0.0f);
			}
		}
		//
		if (other.gameObject.CompareTag ("Trigger")) {
			Trigger t = other.gameObject.GetComponent<Trigger> ();
			t.Enter ();
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		//cannot trigger world portal
		if (other.gameObject.CompareTag ("Trigger")) {
			Trigger t = other.gameObject.GetComponent<Trigger> ();
			t.Stay ();
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		//triggers world portal
		if (other.gameObject.CompareTag ("Trigger")) {
			Trigger t = other.gameObject.GetComponent<Trigger> ();
			t.Exit ();
		}
	}


	public void OnCollisionEnter2D (Collision2D coll)
	{
		//just to check if I collide enemy
		if (coll.gameObject.tag == "Enemy")
			Debug.Log ("I collide the enemy");
	}


	private void HandleAttacks ()
	{//melee attack with cooldown
		if (attack && timeSinceAttack > 0.5f) {
			timeSinceAttack = 0.0f;
			//sound
			source.PlayOneShot (swordSound, 1F);
			//anim which is not animation just sprite
			anim.SetTrigger ("Attack");
			//checks if there are objects in line of player movement axis
			RaycastHit2D[] hitObjects = Physics2D.BoxCastAll (transform.position, new Vector2(meleeRange, meleeRange), 0.0f, new Vector2 (playerSpeedX, playerSpeedY), meleeRange);
			//every object on hit range tagged "Enemy" sends enemyTakeDamage message for receiver
			foreach (RaycastHit2D hit in hitObjects) {
				if (hit.collider.CompareTag("Enemy")) {
					hit.collider.SendMessage ("EnemyTakeDamage", meleeDamage, SendMessageOptions.DontRequireReceiver);
					Debug.Log ("Hit " + hit.collider.name);
				}
			}
		}
	//ranged attack and cooldown
		if (rangedAttack && timeSinceAttack > 1.0f) {
			timeSinceAttack = 0.0f;
			// play shoot sound when do ranged attack
			source.PlayOneShot (shootSound, 1F);
			//if saved direction is Down creates arrow object from prefabs and pushes its rigidbody negative y axis
			anim.SetTrigger("Shoot");
			if (directionD == true) {
				GameObject newArrowObjDown = Instantiate (arrowObjDown, transform.position, transform.rotation);
				newArrowObjDown.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0f, -150f));
			}
			//same up but positive
			if (directionU == true) {
				GameObject newArrowObjUp = Instantiate (arrowObjUp, transform.position, transform.rotation);
				newArrowObjUp.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0f, 150f));
			}
			// same on X axis
			if (directionR == true) {
				GameObject newArrowObjRight = Instantiate (arrowObjRight, transform.position, transform.rotation);
				newArrowObjRight.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (150f,0f));
			}
			// and on negative x axis
			if (directionL == true) {
				GameObject newArrowObjLeft = Instantiate (arrowObjLeft, transform.position, transform.rotation);
				newArrowObjLeft.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (-150f, 0f));
			}
		}
	}
	//enables your melee attack if you have certain item collected
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
	//ables your ranged attack if you have certain item collected
	private void HandleRanged ()
	{
		if (Player.HasItem (Item.BOW)) {
			rangedAttack = true;
			Debug.Log ("You shoot arrow");
		} else {
			rangedAttack = false;
			Debug.Log ("You have no bow");
		}
	}

	private void HandleMovement () {
		
		//flip player sprite
		if (spriteRenderer != null) {
			// if left button is pressed enable sprite renderers flip X
			if (Input.GetPressed (Input.LEFT)) {
				spriteRenderer.flipX = true;
			}
			//if right button is pressed deactivate it
			if (Input.GetPressed (Input.RIGHT)) {
				spriteRenderer.flipX = false;

			}
		}
		//if left is pressed at the same time with input down or up, if right is pressed at the same time with up or down
		if ((Input.GetPressed (Input.LEFT) || Input.GetPressed (Input.RIGHT)) && (Input.GetPressed (Input.DOWN) || Input.GetPressed (Input.UP))) {
			//player mas speed is divided by squareroot (2f)
			playerMaxSpeed = 5.0f/Mathf.Sqrt (2.0f);
		} else {
			//speed is normal
			playerMaxSpeed = 5.0f;
		}
		//Easy movement need to change into method and call it in GameControl
		if (Input.GetPressed (Input.LEFT)) {
			//checks if the playerspeed on X axis is bigger or equal with -playerMaxSpeed
			if (playerSpeedX >= -playerMaxSpeed) {
				//moves player left by decrementing acceleration multiplied by deltatime
				playerSpeedX -= (acceleration * Time.deltaTime);
				//activates the direction currently moving and makes other directions false for shooting
				directionD = false;
				directionU = false;
				directionR = false;
				directionL = true;
				//player.transform.Translate (-playerSpeed, 0, 0);
			}
		} 
		//moves right
		if (Input.GetPressed (Input.RIGHT)) {
			
			if (playerSpeedX <= playerMaxSpeed) {
				
				playerSpeedX += (acceleration * Time.deltaTime);
				directionD = false;
				directionU = false;
				directionL = false;
				directionR = true;
				//	player.transform.Translate (playerSpeed, 0, 0);
			}
		} 
		//moves up
		if (Input.GetPressed (Input.UP)) {
			
			if (playerSpeedY <= playerMaxSpeed) {
				
				playerSpeedY += (acceleration * Time.deltaTime);
				directionD = false;
				directionL = false;
				directionR = false;
				directionU = true;
				//player.transform.Translate (0, playerSpeed, 0);
			}
		} 
		//moves down
		if (Input.GetPressed (Input.DOWN)) {
			
			if (playerSpeedY >= -playerMaxSpeed) {
				
				playerSpeedY -= (acceleration * Time.deltaTime); 
				directionL = false;
				directionU = false;
				directionR = false;
				directionD = true;
			}
		}
		//checks if left and right is not pressed or absolute player speed is higher than max speed and player speed is not 0
		if ((!Input.GetPressed (Input.LEFT) && !Input.GetPressed (Input.RIGHT) || Mathf.Abs(playerSpeedX) > playerMaxSpeed) && playerSpeedX != 0.0) {
			//if its higher than 0
			if (playerSpeedX > 0) {
				//
				playerSpeedX = Mathf.Max (0, playerSpeedX - (deceleration * Time.deltaTime));
			} else {
				//
				playerSpeedX = Mathf.Min (0, playerSpeedX + (deceleration * Time.deltaTime));
			}
		}
		//same as before but on Y axis movement
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
		rangedAttack = false;
	}
}