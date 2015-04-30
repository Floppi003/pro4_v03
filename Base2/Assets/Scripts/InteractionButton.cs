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
	//		Application.LoadLevel ("Button pressed.");
			Debug.Log("Button pressed.");
		}
	}

	public void OnTriggerStay(Collider other) {
			loadPrompt = "[F] for Interaction";
	}

	public void OnTriggerExit() {
		loadPrompt = "";
	}
}
