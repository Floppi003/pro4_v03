

using UnityEngine;
using System.Collections;
using Tobii.EyeX.Framework;


public class Level1 : MonoBehaviour { 

	private IEyeXDataProvider<EyeXEyePosition> _dataProvider;
	private EyeXHost _eyexHost;

	public EyeXEyePosition LastEyePosition { get; private set; }


	private AudioFilesLevel1 audioFiles;

	private float elapsedTime;
	private bool openFirstDoorAnimationStarted;
	private bool wallTriggerStarted;
	private bool wallSoundPlayed;
	private bool wakeupSoundPlayed;
	private bool wallsVisible; // default is false, when user closed eyes for a certain amount of time it will be true

	private float timeTillWakeupSoundStarts;
	private float timeTillFirstDoorOpens;
	private float timeTillWallSoundStarts;
	private float timeEyesAreClosed; // when eyes are closed this will count up. when eyes are opened it will get reset


	void Awake() {
		_eyexHost = EyeXHost.GetInstance();
		_dataProvider = _eyexHost.GetEyePositionDataProvider();

		// check whether left and right eye are closed
		EyeXEyePosition lastEyePosition = _dataProvider.Last;
		
		Debug.Log ("lastEyePosition: " + lastEyePosition);

		Debug.Log ("leftEye: " + lastEyePosition.LeftEye);
		
		if (!lastEyePosition.RightEye.IsValid && !lastEyePosition.LeftEye.IsValid) {
			timeEyesAreClosed += Time.deltaTime;
		}

		timeTillFirstDoorOpens = 0.0f; //9.0f;
		timeTillWallSoundStarts = 0.0f; //3.0f;
		timeTillWakeupSoundStarts = 0.0f; //2.0f;
		timeEyesAreClosed = 0.0f;
		
		wakeupSoundPlayed = false;
		openFirstDoorAnimationStarted = false;
		wallTriggerStarted = false;
		wallSoundPlayed = false;
		Debug.Log ("Awake");
	}

	void Start() {
		audioFiles = GameObject.Find ("GM").GetComponent<AudioFilesLevel1> ();

		// disable the rendering of the invisible walls
		GameObject.Find ("Level_1_Obstacle_Wall_01").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find ("Level_1_Obstacle_Wall_02").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find ("Level_1_Obstacle_Wall_03").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find ("Level_1_Obstacle_Wall_04").GetComponent<MeshRenderer>().enabled = false;

		Debug.Log ("Start");
	}

	void Update() {
		elapsedTime += Time.deltaTime;

		// play the wakeup sound
		if (timeTillWakeupSoundStarts < elapsedTime && wakeupSoundPlayed == false) {
			wakeupSoundPlayed = true;

			Debug.Log ("Wakeup sound queued");
			AudioManager.instance.playAudioClipForced(audioFiles.wakeupAudioClip);
		}


		// If the wall trigger was triggered then start the countdown till the voice should start
		if (wallTriggerStarted) {
			timeTillWallSoundStarts -= Time.deltaTime;

			if (wallSoundPlayed == false && timeTillWallSoundStarts < 0) {
				// play Audio Sound
				wallSoundPlayed = true;
				AudioManager.instance.playAudioClipForced(audioFiles.firstWallAudioClip);
			}
		}


		// open the door if the time is right!
		if (elapsedTime > timeTillFirstDoorOpens && !openFirstDoorAnimationStarted) {
			openFirstDoorAnimationStarted = true;
			GameObject.Find ("FirstDoor").GetComponent<Animator>().Play ("OpenDoor");
		}


		// after the firstWallAudioClip was played check if the player closes his eyes for a second
		if (wallSoundPlayed && !wallsVisible) {

			// check whether left and right eye are closed
			EyeXEyePosition lastEyePosition = _dataProvider.Last;

			Debug.Log ("lastEyePosition: " + lastEyePosition);

			if (!lastEyePosition.RightEye.IsValid && !lastEyePosition.LeftEye.IsValid) {
				timeEyesAreClosed += Time.deltaTime;
			} else {
				timeEyesAreClosed = 0.0f;
			}

			if (timeEyesAreClosed > 1.0f) {
				// show the walls
				GameObject.Find ("\tLevel_1_Obstacle_Wall_01").GetComponent<MeshRenderer>().enabled = true;
				GameObject.Find ("Level_1_Obstacle_Wall_02").GetComponent<MeshRenderer>().enabled = true;
				GameObject.Find ("Level_1_Obstacle_Wall_03").GetComponent<MeshRenderer>().enabled = true;
				GameObject.Find ("Level_1_Obstacle_Wall_04").GetComponent<MeshRenderer>().enabled = true;

				wallsVisible = true;
			}
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