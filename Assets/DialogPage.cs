using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPage {

	private string text;
	private DialogButton[] buttons;
	private bool large;
	private int fontSize;

	public DialogPage (string text, DialogButton[] buttons, bool large, int fontSize) : this(text, buttons, large) {
		this.fontSize = fontSize;
	}

	public DialogPage (string text, DialogButton[] buttons, bool large) : this(text, buttons) {
		this.large = large;
	}

	public DialogPage (string text, DialogButton[] buttons) {
		this.text = text;
		this.buttons = buttons;
	}
	void Start () {
		
	}

	public int GetButtonCount() {
		return buttons.Length;
	}
	
	public int GetButtonTarget(int button) {
		return buttons [button].GetTarget ();
	}

	public string GetButtonText(int button) {
		return buttons [button].GetText ();
	}

	public string GetPageText() {
		return text;
	}

	public DialogButton[] GetButtons() {
		return buttons;
	}
}
