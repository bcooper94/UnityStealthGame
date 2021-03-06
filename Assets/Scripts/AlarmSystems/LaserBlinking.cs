﻿using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {
	public float onTime;
	public float offTime;

	private float timer;

	void Update() {
		timer += Time.deltaTime;

		if (GetComponent<Renderer>().enabled && timer >= onTime) {
			SwitchBeam();
		}
		if (!GetComponent<Renderer> ().enabled && timer >= offTime) {
			SwitchBeam();
		}
	}

	void SwitchBeam() {
		timer = 0f;
		Renderer renderer = GetComponent<Renderer> ();
		Light light = GetComponent<Light> ();

		renderer.enabled = !renderer.enabled;
		light.enabled = !light.enabled;
	}
}
