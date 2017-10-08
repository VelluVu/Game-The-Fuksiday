using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public const int GAMECANVAS = 0;
	public const int DIALOGCANVAS = 1;
	public const int DEATHCANVAS = 2;

	private static List<Canvas> canvases;

	static List<Item> allItems;
	public static Player player;
	DialogController dc;
	static float textTimer = 0.0f;
	Text output;
	void Start() {
		canvases = new List<Canvas> ();
		canvases.Add (GameObject.Find ("Canvas").GetComponent<Canvas> ());
		canvases.Add (GameObject.Find ("DialogCanvas").GetComponent<Canvas> ());
		canvases.Add (GameObject.Find ("DeathScreen").GetComponent<Canvas> ());
		dc = canvases[DIALOGCANVAS].GetComponentInParent<DialogController> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();
		dc.StartDialog (new DialogLoader ().LoadJson ("IntroDialog"));



		allItems = new List<Item> ();
		allItems.Add (GameObject.Find ("Wrench").GetComponent<Item> ());
		allItems.Add (GameObject.Find ("OilCan").GetComponent<Item> ());
		allItems.Add (GameObject.Find ("Lever").GetComponent<Item> ());
		allItems.Add (GameObject.Find ("Sword").GetComponent<Item> ());
		allItems.Add (GameObject.Find ("GunPowder").GetComponent<Item> ());
		allItems.Add (GameObject.Find ("JesusTape").GetComponent<Item> ());
		allItems.Add (GameObject.Find ("ToiletPaperRoll").GetComponent<Item> ());
		allItems.Add (GameObject.Find ("Jumpsuit").GetComponent<Item> ());

		output = GameObject.Find ("TextOutput").GetComponent<Text> ();
	}

	void Update() {
		
		textTimer -= Time.deltaTime * 1000;
		output.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Min (1000.0f, textTimer) / 1000.0f);
	}

	public static void PlayerDied() {
		SetCanvas (DEATHCANVAS);
	}



	public static void SetCanvas(int canvas) {
		Debug.Log (canvas);
		foreach (Canvas c in canvases) {
			c.enabled = false;
		}
		canvases [canvas].enabled = true;
	}

	public static Player GetPlayer() {
		return player;
	}
	public static List<Item> GetAllItems() {
		return allItems;
	}

	public static void ShowText (string text) {
		Text output = GameObject.Find ("TextOutput").GetComponent<Text> ();
		output.text = text;
		textTimer = 4000;
	}

}