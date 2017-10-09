using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour {

	bool shake = false;
	
	// Update is called once per frame
	void Update () {
		if (shake) {
			this.transform.position = new Vector3 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), -10.0f);
		}
	}

	public void StartShake() {
		shake = true;
	}
}
