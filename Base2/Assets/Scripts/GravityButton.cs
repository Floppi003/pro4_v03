using UnityEngine;
using System.Collections;

public class GravityButton : MonoBehaviour {

	public Vector3 gravityVector;
	public GravityBody player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	void OnCollisionEnter(Collider other){
		if (other.tag == "Player") {
			Debug.Log ("Player detected");
			
			
			player.gravityUp=gravityVector;
		}
		Debug.Log ("OnTriggerEnter");
		//other.gameObject.GetComponent<AudioSource> ().PlayOneShot (other.gameObject.GetComponent<FirstPersonController>().helloClip);

	}
*/
	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			Debug.Log ("Player detected");

			player.gravityUp=gravityVector;
		}
		Debug.Log ("OnTriggerEnter");
		//other.gameObject.GetComponent<AudioSource> ().PlayOneShot (other.gameObject.GetComponent<FirstPersonController>().helloClip);
	}
}
