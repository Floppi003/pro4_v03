using UnityEngine;
using System.Collections;

public class InteractionButton : MonoBehaviour {
	
	private string loadPrompt;
	private bool pressButton;
	public GameObject exploder;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	public void Update () {
	}

	public void playAnimation() {
		if (Input.GetButtonDown ("ButtonPressed")) {
			GetComponent<Animator> ().Play ("Push");
		//	Debug.Log("Button pressed.");

		// öffne die Tür
			exploder.GetComponent<Explosion>().Explode();
		}
	}
}
