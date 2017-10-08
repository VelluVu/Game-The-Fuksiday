using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButton {

	private string text;
	private int target;
	private string result = null;

	public DialogButton (string text, int target, string result = null) {
		this.text = text;
		this.target = target;
		if (!(result == null)) {
			this.result = result;
		}
	}

	public string GetText() {
		return this.text;
	}

	public string GetResult() {
		return this.result;
	}

	public int GetTarget() {
		return this.target;
	}

	public bool HasResult() {
		return !(result == null);
	}
}
