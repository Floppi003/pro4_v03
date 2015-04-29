using UnityEngine;
using System.Collections.Generic;
using System.Timers;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	private static Queue<AudioClip> audioQueue;
	private static AudioSource audioSource;
	private static float timeSinceLastPlay; // in seconds
	private float timeOfLastPlayedClip; // in seconds
 
	// Singleton Methods
	public static AudioManager instance {
		get {
			if(_instance == null) {
				Debug.LogError ("Creating Instance!");
				
				//Tell unity not to destroy this object when loading a new scene!
				//DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	
	void Awake() {
		if(_instance == null) {
			//If I am the first instance, make me the Singleton
			_instance = this;
			audioQueue = new Queue<AudioClip>();
			audioSource = GameObject.Find ("Player").GetComponent<AudioSource>();
			//DontDestroyOnLoad(this);
		}
		else {
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			/*if(this != _instance)
				Destroy(this.gameObject);*/
		}
	}

	protected void Update() {
		timeSinceLastPlay += Time.deltaTime;
	}


	public void queueAudioClip(AudioClip audioClip) {
		Debug.Log ("queue audio clip called");
		// calculate time for next audio playback
		AudioClip[] audioClips = audioQueue.ToArray();
		float totalWaitingTime = 0.0f; // seconds
		foreach (AudioClip ac in audioClips) {
			totalWaitingTime += ac.length;
		}
			
		totalWaitingTime += Mathf.Max (0, timeOfLastPlayedClip - timeSinceLastPlay);
		int totalWaitingTimeInt = (int) totalWaitingTime * 1000;

		audioQueue.Enqueue(audioClip);
		Invoke ("playNextClipInQueue", totalWaitingTime);
	}

	public bool playAudioClipIfFree(AudioClip audioClip) {
		if (!audioSource.isPlaying) {
			timeOfLastPlayedClip = audioClip.length;
			timeSinceLastPlay = 0.0f;

			audioSource.PlayOneShot (audioClip);
			return true;
		}

		return false;
	}

	public void playAudioClipForced(AudioClip audioClip) {
		//  stop all other playback and delete the queue
		audioQueue.Clear ();
		audioSource.Stop ();

		timeOfLastPlayedClip = audioClip.length;
		timeSinceLastPlay = 0.0f;

		audioSource.PlayOneShot (audioClip);
	}


	private void playNextClipInQueue() {
		Debug.Log ("Playing next shot out of queue: ");

		// save the time and lenght of the clip that is going to be played
		timeOfLastPlayedClip = audioQueue.Peek ().length;
		timeSinceLastPlay = 0.0f;

		// play the audio clip
		audioSource.PlayOneShot (audioQueue.Dequeue ());
	}
}
