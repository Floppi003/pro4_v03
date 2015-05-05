

using UnityEngine;
using System.Collections;
//using AudioManager;

public class Level1 : MonoBehaviour { 

	private AudioFilesLevel1 audioFiles;

	private float elapsedTime;
	private bool openFirstDoorAnimationStarted;
	private bool wallTriggerStarted;
	private bool wallSoundPlayed;
	private bool wakeupSoundPlayed;

	private float timeTillFirstDoorOpens;
	private float timeTillWallSoundStarts;


	void Awake() {
		timeTillFirstDoorOpens = 9.0f;
		timeTillWallSoundStarts = 3.0f;
		
		wakeupSoundPlayed = false;
		openFirstDoorAnimationStarted = false;
		wallTriggerStarted = false;
		wallSoundPlayed = false;
		Debug.Log ("Awake");
	}

	void Start() {
		audioFiles = GameObject.Find ("AudioFilesLevel1").GetComponent<AudioFilesLevel1> ();

		Debug.Log ("Start");
	}

	void Update() {
		elapsedTime += Time.deltaTime;

		// play the wakeup sound
		if (wakeupSoundPlayed == false) {
			wakeupSoundPlayed = true;
			AudioClip ac = new AudioClip();
//			AudioManager.instance.queueAudioClip(ac);
		}

		// If the wall trigger was triggered then start the countdown till the voice should start
		if (wallTriggerStarted) {
			timeTillWallSoundStarts -= Time.deltaTime;

			if (wallSoundPlayed == false && timeTillWallSoundStarts < 0) {
				// play Audio Sound
				wallSoundPlayed = true;
//				AudioManager.instance.queueAudioClip(audioFiles.firstWallAudioClip);
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