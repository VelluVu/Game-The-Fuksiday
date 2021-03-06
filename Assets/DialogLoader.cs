﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class DialogLoader {
	public Dialog LoadJson(string name) {

		//Load text file and convert it to JSON data
		string json = (Resources.Load (name) as TextAsset).ToString();
		JsonData jsondata = JsonMapper.ToObject (json);


		int pageCount = jsondata.Count;
		//Temporary array for dialog page data
		DialogPage[] pages = new DialogPage[pageCount]; 
		//Temporary dictionary for arrays of buttons for each of the dialog pages
		Dictionary<int, DialogButton[]> buttonArrays = new Dictionary<int, DialogButton[]> (); 
		//Temporary dictionary for the texts on each dialog page
		Dictionary<int, string> pageTexts = new Dictionary<int, string> ();

		for (int i = 0; i < pageCount; i++) {
			//Create actual button objects for every dialog page
			int size = jsondata [i]["buttons"].Count;
			DialogButton[] buttons = new DialogButton[size];

			for (int j = 0; j < size; j++) { //Actually create the button objects for a specific page and put them in an array
				buttons [j] = new DialogButton ((string)(jsondata [i] ["buttons"] [j] ["text"]), (int)(jsondata [i] ["buttons"] [j] ["target"]), (jsondata [i] ["buttons"] [j].Keys.Contains ("result")) ? (string)(jsondata [i] ["buttons"] [j] ["result"]) : null);
			}
			buttonArrays.Add (i, buttons);
			//Add the resulting array to the dictionary
			pageTexts.Add (i, (string)(jsondata [i]["text"]));
		}

		for (int i = 0; i < pageCount; i++) {
			pages [i] = new DialogPage (pageTexts [i], buttonArrays [i]);
			//Combine the text and button data into a single object and put it into a temporary array
		}
		return new Dialog(pages); //Create the Dialog object using the temporary array
	}


	public Dialog loadTest() {

		DialogPage[] pages = new DialogPage[4];

		DialogButton[] buttons1 = new DialogButton[2];
		buttons1 [0] = new DialogButton ("Joo", 2, "lol");
		buttons1 [1] = new DialogButton ("Ei", 3, "lol");

		DialogButton[] buttons2 = new DialogButton[1];
		buttons2 [0] = new DialogButton ("Aha", 0, "lol");

		pages [1] = new DialogPage ("Hello World!", buttons1);
		pages [2] = new DialogPage ("Painoit joo", buttons2);
		pages [3] = new DialogPage ("Painoit ei", buttons2);

		return new Dialog (pages);
	}
}