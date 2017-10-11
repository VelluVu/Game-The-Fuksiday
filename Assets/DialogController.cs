using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

	private Dialog dialog;
	private int page;
	private bool large;
	private Button[] buttons = new Button[3];
	private Image[] buttonImage = new Image[3];
	private Text[] buttonTexts = new Text[3];
	private int[] buttonTargets = new int[3];
	private Text dialogText;
	ArrayList results;

	public void Start() {
		buttons [0] = GameObject.Find ("DialogButton1").GetComponent<Button> ();
		buttons [1] = GameObject.Find ("DialogButton2").GetComponent<Button> ();
		buttons [2] = GameObject.Find ("DialogButton3").GetComponent<Button> ();

		buttonTexts [0] = GameObject.Find ("Button1Text").GetComponent<Text> ();
		buttonTexts [1] = GameObject.Find ("Button2Text").GetComponent<Text> ();
		buttonTexts [2] = GameObject.Find ("Button3Text").GetComponent<Text> ();

		buttonImage [0] = GameObject.Find ("DialogButton1").GetComponent<Image> ();
		buttonImage [1] = GameObject.Find ("DialogButton2").GetComponent<Image> ();
		buttonImage [2] = GameObject.Find ("DialogButton3").GetComponent<Image> ();

		dialogText = GameObject.Find ("DialogText").GetComponent<Text> ();

		buttons [0].onClick.AddListener (() => clickButton (0));
		buttons [1].onClick.AddListener (() => clickButton (1));
		buttons [2].onClick.AddListener (() => clickButton (2));

		results = new ArrayList ();
		results.Add (new KeyValuePair<string, UnityEngine.Events.UnityAction> ("jumpsuit", () => GameController.GetPlayer().AddToInventory(GameController.GetAllItems()[Item.JUMPSUIT])));
	}

	private void clickButton(int button) {
		//Run code from ArrayList results if the clicked button has a result specified
		if (dialog.GetButtons (page) [button].GetResult () != null) {
			foreach (KeyValuePair<string, UnityEngine.Events.UnityAction> kv in results) {
				if (kv.Key.Equals (dialog.GetButtons (page) [button].GetResult ())) {
					kv.Value.Invoke ();
				}
			}
		}
		//Button target page -1 is used to exit the dialog and return to gameplay.
		if (buttonTargets [button] == -1) {
			ExitDialog ();
		} else {
			showPage (buttonTargets [button]);
		}
	}

	public DialogController() {

	}

	public DialogController (Dialog dialog) {
		this.dialog = dialog;
	}

	public void StartDialog(Dialog dialog, int page = 0) {
		this.page = page;
		this.dialog = dialog;
		GameController.SetCanvas (GameController.DIALOGCANVAS);
		Input.SetBlockInput (true);
		showPage (this.page);
	}

	public void ExitDialog() {
		//Close the dialog and switch back to normal gameplay
		this.page = -1;
		GameController.SetCanvas (GameController.GAMECANVAS);
		this.dialog = null;
		Input.SetBlockInput (false);
	}

	private void showPage(int page) {
		this.page = page;
		dialogText.text = dialog.GetText (page);
		for (int i = 0; i<3; i++) {
			//Disable all response buttons
			buttons [i].enabled = false;
			buttonImage [i].enabled = false;
			buttonTexts [i].text = "";

		}
		for (int i = 0; i < dialog.GetButtons (page).Length; i++) {
			//Re-enable response buttons required by the current dialog page
			DialogButton button = dialog.GetButtons(page) [i];
			setButton (button.GetText (), i, button.GetTarget ());
			buttons [i].enabled = true;
			buttonImage [i].enabled = true;
		}

	}

	private void setButton(string text, int button, int targetPage) {
		//Set a target page and a text for a dialog response button
		buttonTexts [button].text = text;
		buttonTargets [button] = targetPage;
	}
}