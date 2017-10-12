using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog {

	DialogPage[] pages;
	public Dialog (DialogPage[] pages) {
		this.pages = pages;
	}

	public string GetText(int page) {
		//Get text from a dialog page
		return pages [page].GetPageText ();
	}

	public DialogButton[] GetButtons(int page) {
		//Get an array of dialog buttons from a page
		return pages [page].GetButtons ();
	}
}
