using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Vector2 velocity;

	[SerializeField]
	private float yMax = 7;
	[SerializeField]
	private float yMin = 0;
	[SerializeField]
	private float xMax = 15;
	[SerializeField]
	private float xMin = -7;

	private Transform target;


	void Start () {
		target = GameObject.Find("Player").transform;
	
	}
	// Update is called once per frame
	void LateUpdate () {

		transform.position = new Vector3(Mathf.Clamp(target.position.x,xMin,xMax), 
			Mathf.Clamp(target.position.y,yMin,yMax), transform.position.z);
	}
}
