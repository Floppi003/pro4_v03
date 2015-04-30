using UnityEngine;
using System.Collections;

public class InteractionButton : MonoBehaviour {
	
	private string loadPrompt;
	private bool pressButton;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	public void Update () {
		if (Input.GetButtonDown("ButtonPressed")) 
		{
			Debug.Log("Button pressed.");
			playAnimation();

	//		Application.LoadLevel ("Button pressed.");
		}
	}

	void playAnimation() {
		GetComponent<Animator> ().Play("Push");

	}
}
