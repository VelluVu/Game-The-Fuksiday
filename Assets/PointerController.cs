using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerController : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler {

	private bool pointerDown = false;
	private bool insideArea = false;

	void Update() {
		pointerDown = UnityEngine.Input.GetMouseButton (0);
	}

	public void OnPointerExit(PointerEventData eventData) {
		insideArea = false;
	}
	public void OnPointerEnter (PointerEventData eventData) {
		insideArea = true;
	}

	public bool GetPointerDown() {
		return pointerDown && insideArea;
	}
}

