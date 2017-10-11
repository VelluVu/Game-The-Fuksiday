using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour {

	static bool blockInput = false;

	public const int LEFT = 1;
	public const int RIGHT = 2;
	public const int UP = 3;
	public const int DOWN = 4;
	//directions
	static PointerController bUp;
	static PointerController bDown;
	static PointerController bLeft;
	static PointerController bRight;
	static PointerController bUL;
	static PointerController bUR;
	static PointerController bDL;
	static PointerController bDR;

	void Start() {
		bUp = GameObject.Find ("ButtonUp").GetComponent<PointerController> ();
		bDown = GameObject.Find ("ButtonDown").GetComponent<PointerController> ();
		bLeft = GameObject.Find ("ButtonLeft").GetComponent<PointerController> ();
		bRight = GameObject.Find ("ButtonRight").GetComponent<PointerController> ();
		bUL = GameObject.Find ("ButtonUpLeft").GetComponent<PointerController> ();
		bUR = GameObject.Find ("ButtonUpRight").GetComponent<PointerController> ();
		bDL = GameObject.Find ("ButtonDownLeft").GetComponent<PointerController> ();
		bDR = GameObject.Find ("ButtonDownRight").GetComponent<PointerController> ();
	}

	public static void SetBlockInput(bool blockInput) {
		Input.blockInput = blockInput;
	}
	//checks if button is pressed
	public static bool GetPressed(int btn_id){
		if (blockInput)
			return false;
		switch (btn_id) {
		case LEFT:
			return bLeft.GetPointerDown () || bDL.GetPointerDown () || bUL.GetPointerDown ();
		case RIGHT:
			return bRight.GetPointerDown () || bDR.GetPointerDown () || bUR.GetPointerDown ();
		case UP:
			return bUp.GetPointerDown () || bUL.GetPointerDown () || bUR.GetPointerDown ();
		case DOWN:
			return (bDown.GetPointerDown () || bDL.GetPointerDown () || bDR.GetPointerDown ());
		default:
			return false;
		}
	}
}