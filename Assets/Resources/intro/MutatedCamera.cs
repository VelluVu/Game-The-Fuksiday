using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutatedCamera : MonoBehaviour {

	bool active;
	public void Activate () {
		active = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			this.transform.Translate (new Vector3 (0.0f, -Time.deltaTime, 0.0f));
		}
	}
}
