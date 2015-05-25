using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float smoothness = 1.5f;

	private Transform player;
	private Vector3 relCameraPos;
	private float relCameraPosMag;
	private Vector3 newPos;

	void Awake() {
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
		relCameraPos = GetComponent<Transform> ().position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}

	void FixedUpdate() {
		Vector3 standardPos = player.position + relCameraPos;
		Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;

	}
}
