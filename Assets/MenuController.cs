using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	Button startButton;
	Button exitButton;
	GameObject bg;

	void Start () {
		startButton = GameObject.Find ("ButtonStart").GetComponent<Button> ();
		exitButton = GameObject.Find ("ButtonExit").GetComponent<Button> ();
		bg = GameObject.Find ("Quad");

		startButton.onClick.AddListener(() => startGame());
		exitButton.onClick.AddListener(() => exitGame());
	}
	
	// Update is called once per frame
	void startGame () {
		SceneManager.LoadScene ("GameWorld");
	}
	void exitGame () {
		Application.Quit ();
	}

	void Update() {
		bg.transform.Rotate (new Vector3(0,0,2f*Time.deltaTime));
	}
}
