using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour {

	public Missile missile;
	public Camera camera;
	public Camera blackCamera;
	public Camera missileCamera;
	public Camera europeCamera;
	public Camera mutatedCamera;
	public Button hugeButton;
	public Button skipButton;
	public Text buttonText;
	int part = 0;
	public Text text;
	public Text text2;
	float timeSinceTouch = 2.0f;

	float time=0.0f;
	// Use this for initialization
	void Start () {
		text2.text = "A few months ago an intercontinental ballistic missile was fired from Pyongyang, the capital of North Korea.";
		//hugeButton.onClick.AddListener (() => bigButtonPressed ());
		skipButton.onClick.AddListener (() => skipIntro ());
	}

	void skipIntro() {
		SceneManager.LoadScene ("GameWorld");
	}

	void bigButtonPressed() {
		timeSinceTouch = 0.0f;
		Debug.Log ("adsasdAds");
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		timeSinceTouch += Time.deltaTime;

		if (time > 6 && part < 1) {
			part = 1;
			camera.enabled = true;
			blackCamera.enabled = false;
			text.text = "Pyongyang, DPRK\n5:59 AM";
			text2.text = "";
		}
		if (time > 8.0f && part < 2) {
			part = 2;
			missile.Launch ();
			camera.GetComponentInParent<IntroCamera> ().StartShake ();
			text.text = "Pyongyang, DPRK\n6:00 AM";
		}
		if (time > 12.0f && part < 3) {
			part = 3;
			camera.enabled = false;
			blackCamera.enabled = true;
			text.text = "";
			text2.text = "It was supposed to land in the United States.";
		}
		if (time > 16.0f && part < 4) {
			part = 4;
			blackCamera.enabled = false;
			missileCamera.enabled = true;
			missileCamera.GetComponentInParent<MissileCamera> ().enable ();
			text2.text = "";
		}
		if (time > 20.0f && part < 5) {
			part = 5;
			blackCamera.enabled = true;
			missileCamera.enabled = false;
			text2.text = "However, due to a huge miscalculation, it struck southwestern Finland.";
		}
		if (time > 24.0f && part < 6) {
			part = 6;
			blackCamera.enabled = false;
			europeCamera.enabled = true;
			text2.text = "";
		}
		if (time > 26.0f && part < 7) {
			part = 7;
			blackCamera.enabled = true;
			europeCamera.enabled = false;
			text2.text = "The world descended into chaos with everyone firing nukes at each other.";
		}
		if (time > 30.0f && part < 8) {
			part = 8;
			text2.text = "Due to the fallout from the war all plants died and animals started mutating.";
		}
		if (time > 34.0f && part < 9) {
			part = 9;
			blackCamera.enabled = false;
			mutatedCamera.enabled = true;
			mutatedCamera.GetComponentInParent<MutatedCamera> ().Activate ();
			text2.text = "";
		}
		if (time > 37.0f && part < 10) {
			part = 10;
			mutatedCamera.enabled = false;
			blackCamera.enabled = true;
			text2.text = "Somehow the people in Finland managed to keep living life as if nothing happened.";
		}
		if (time > 41.0f && part < 11) {
			part = 11;
			text2.text = "Today is your first day at Helsinki Metropolia University of Applied Sciences in Leppävaara.";
		}
		if (time > 46.0f) {
			skipIntro (); //Exit the intro sequence and load the GameWorld scene.
		}
	}
}
