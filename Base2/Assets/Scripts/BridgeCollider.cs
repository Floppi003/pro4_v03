using UnityEngine;
using System.Collections;

public class BridgeCollider : MonoBehaviour {

	public string objectName;

	void OnTriggerEnter(Collider other) {
		Debug.Log ("OnTriggerEnter");
		//other.gameObject.GetComponent<AudioSource> ().PlayOneShot (other.gameObject.GetComponent<FirstPersonController>().helloClip);

		if (objectName.Equals ("BridgeFloorless")) {
			if (other.gameObject.name.Equals ("Player")) {
				Debug.Log ("bridge floor less collider");
				FirstPersonController fps = GameObject.Find ("Player").GetComponent<FirstPersonController>();
				if(!fps.isInFellofZone) {
					Debug.Log ("will call audio collider");
					/*AudioManager.instance.queueAudioClip(fps.fellofBridgeSound());
				AudioManager.instance.queueAudioClip (fps.fellofBridgeSound());
				AudioManager.instance.queueAudioClip (fps.fellofBridgeSound());
				*/
					Debug.LogWarning ("bridgeflorless playing sound");
					fps.playFellofBridgeSound ();
					
				}
				fps.isInFellofZone = true;
				fps.fellOfBridgeCounter++;
			}
			
		} else if (objectName.Equals ("RedEyeLeftEyeFloorlessCollider")) {
			if (other.gameObject.name.Equals ("Player")) {
				FirstPersonController fps = GameObject.Find ("Player").GetComponent<FirstPersonController>();
				if(!fps.isInFellofZone) {
					fps.playFellofGeneralSound ();
				}
				Debug.LogWarning ("red eye left eye sound played");
				fps.isInFellofZone = true;
				fps.fellOfBridgeCounter++;
			}
		}
	}
	
	void OnTriggerStay(Collider other) {
		//Debug.Log ("onTriggerStay");

		// if it is the bridge, move the player slightly
		if (objectName.Equals ("Bridge")) {
			float randomNumber = Random.Range (-0.1f, 0.00f);
			Vector3 position = other.gameObject.transform.position;
			other.gameObject.transform.position = new Vector3 (position.x, position.y, position.z + randomNumber);

			// if it is the floor below the bridge, move the player back to the beginning
		}
	}
	
	void OnTriggerExit(Collider other) {
		
	}
	
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
