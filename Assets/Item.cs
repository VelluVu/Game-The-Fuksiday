using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public int id;
	public string visibleName;

	public static int WRENCH = 0;
	public static int OIL_CAN = 1;
	public static int LEVER = 2;
	public static int SWORD = 3;
	public static int GUNPOWDER = 4;
	public static int JESUSTAPE = 5;
	public static int TOILETPAPERROLL = 6;
	public static int JUMPSUIT = 7;

	public Item(int id, string name) {
		this.id = id;
		visibleName = name;
	}

	public int GetId() {
		return id;
	}
	public string GetVisibleName() {
		return visibleName;
	}
}
 