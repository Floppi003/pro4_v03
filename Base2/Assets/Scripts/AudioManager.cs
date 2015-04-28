using UnityEngine;
using System.Collections.Generic;
using System.Timers;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	private static Queue<AudioClip> audioQueue;
	private static AudioSource audioSource;

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


	public void queueAudioClip(AudioClip audioClip) {
		Debug.Log ("queue Audio Clip called");
		// Queue the audioSource

		// calculate time for next audio playback
		AudioClip[] audioClips = audioQueue.ToArray();
		float totalWaitingTime = 0.0f; // seconds
		foreach (AudioClip ac in audioClips) {
			totalWaitingTime += ac.length;
		}
			
		int totalWaitingTimeInt = (int) totalWaitingTime * 1000;

		audioQueue.Enqueue(audioClip);
		Invoke ("playNextClipInQueue", totalWaitingTime);
		Debug.Log ("totalWaitingTime: " + totalWaitingTime);
	}

	public void playAudioClipIfFree(AudioClip audioClip) {
		
	}

	public void playAudioClipOnceOnly(AudioClip audioCip) {

	}


	private void playNextClipInQueue() {


		Debug.Log ("Playing next shot out of queue: ");

		GameObject.Find ("Player").GetComponent<AudioSource>().PlayOneShot (audioQueue.Dequeue ());
	}
}
