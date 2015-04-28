using UnityEngine;
using System.Collections;

public class Maze_Collider : MonoBehaviour {

	public Vector3 angle;
	public Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Player detected");

		}
		Debug.Log ("OnTriggerEnter");
	}
}
