using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	bool launched = false;
	float velocity = 0.0f;

	public void Launch() {
		launched = true;
	}
	void Update () {
		if (launched) {
			velocity += 0.075f * Time.deltaTime * (velocity*3.0f + 0.5f);
		}
		transform.Translate (new Vector3 (0.0f, velocity, 0.0f));
	}
}