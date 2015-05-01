

using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour { 


	private float elapsedTime;
	private bool openFirstDoorAnimationStarted;
	private bool wallTriggerStarted;
	private bool wallSoundPlayed;

	private float timeTillFirstDoorOpens;
	private float timeTillWallSoundStarts;


	void Awake() {
		timeTillFirstDoorOpens = 9.0f;
		timeTillWallSoundStarts = 3.0f;

		openFirstDoorAnimationStarted = false;
		wallTriggerStarted = false;
		wallSoundPlayed = false;
		Debug.Log ("Awake");
	}

	void Start() {
		Debug.Log ("Start");
	}

	void Update() {
		elapsedTime += Time.deltaTime;

		// If the wall trigger was triggered then start the countdown till the voice should start
		if (wallTriggerStarted) {
			timeTillWallSoundStarts -= Time.deltaTime;

			if (wallSoundPlayed == false) {
				// play Audio Sound
				AudioManager.instance.queueAudioClip(null);
			}
		}

		if (elapsedTime > timeTillFirstDoorOpens && !openFirstDoorAnimationStarted) {
			openFirstDoorAnimationStarted = true;
			GameObject.Find ("FirstDoor").GetComponent<Animator>().Play ("OpenDoor");
		}
	}


	// Triggers
	void OnTriggerEnter(Collider other) {
		Debug.Log ("OnTriggerEnter");
		wallTriggerStarted = true;
	}
	
	void OnTriggerStay(Collider other) {
		Debug.Log ("OnTriggerStay");
	}
	
	void OnTriggerExit(Collider other) {
		Debug.Log ("OnTriggerExit");
	}
}