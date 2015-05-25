using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour {
	public Vector3 position = new Vector3(1000f, 1000f, 1000f);
	public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
	public float lightHighIntensity = 0.25f;
	public float lightLowIntensity = 0f;
	public float fadeSpeed = 7f;
	public float musicFadeSpeed = 1f;

	private AlarmLight alarm;
	private Light mainLight;
	private AudioSource panicAudio;
	private AudioSource[] sirens;

	void Awake() {
		alarm = GameObject.FindGameObjectWithTag (Tags.alarm).GetComponent<AlarmLight> ();
		mainLight = GameObject.FindGameObjectWithTag (Tags.mainLight).GetComponent<Light> ();
		panicAudio = GetComponent<Transform> ().Find ("SecondaryMusic").GetComponent<AudioSource> ();

		GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag (Tags.siren);
		sirens = new AudioSource[sirenGameObjects.Length];

		for (int i = 0; i < sirenGameObjects.Length; i++) {
			sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
		}
	}

	void Update() {
		SwitchAlarms ();
		MusicFading ();
	}

	void SwitchAlarms() {
		alarm.alarmOn = position != resetPosition;

		float newIntensity;

		if (position != resetPosition) {
			newIntensity = lightLowIntensity;
		}
		else {
			newIntensity = lightHighIntensity;
		}

		mainLight.intensity = Mathf.Lerp (mainLight.intensity, newIntensity, Time.deltaTime * fadeSpeed);

		for (int i = 0; i < sirens.Length; i++) {
			if (position != resetPosition && !sirens[i].isPlaying) {
				sirens[i].Play();
			}
			else if (position == resetPosition) {
				sirens[i].Stop();
			}
		}
	}

	void MusicFading() {
		AudioSource music = GetComponent<AudioSource> ();

		if (position != resetPosition) {
			music.volume = Mathf.Lerp (music.volume, 0f, musicFadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
		}
		else {
			music.volume = Mathf.Lerp(music.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
		}
	}
}
