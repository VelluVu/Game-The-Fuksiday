using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuropeCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (-100.0f + Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), -10.0f);
	}
}
