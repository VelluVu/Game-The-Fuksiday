using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPortal : MonoBehaviour {

	public double targetX;
	public double targetY;
	public bool enabled;

	public void SetEnabled(bool enabled) {
		this.enabled = enabled;
	}

	public void setTargetPos(double x, double y) {
		this.targetX = x;
		this.targetY = y;
	}

	public bool isEnabled() {
		return enabled;
	}

	public double getX() {
		return targetX;
	}
	public double getY() {
		return targetY;
	}
}
