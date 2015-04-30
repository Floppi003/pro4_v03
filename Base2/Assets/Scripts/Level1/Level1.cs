

using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour { 

	private float elapsedTime;
	private bool openFirstDoorAnimationStarted;

	void Awake() {
		openFirstDoorAnimationStarted = false;
	}

	void Start() {}

	void Update() {
		elapsedTime += Time.deltaTime;

		if (elapsedTime > 3.0f && !openFirstDoorAnimationStarted) {
			Debug.Log ("animation started");
			openFirstDoorAnimationStarted = true;
			GameObject.Find ("FirstDoor").GetComponent<Animator>().Play ("OpenDoor");
		}
	}

}