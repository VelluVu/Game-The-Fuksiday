using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : Trigger {
	public string DialogName;
	public int TriggerCount = 1;
	public int startPage = 0;
	int currentCount = 0;
	public bool InfiniteTriggers = false;

	public override void Enter () {
		if (currentCount < TriggerCount || InfiniteTriggers) {
			DialogController dc = GameObject.Find ("DialogCanvas").GetComponent<DialogController> ();
			dc.StartDialog (new DialogLoader ().LoadJson (DialogName), startPage);
			currentCount++;
		}

	}
}
