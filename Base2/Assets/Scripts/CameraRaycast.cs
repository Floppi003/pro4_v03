﻿using UnityEngine;
using System.Collections;

public class CameraRaycast : MonoBehaviour {

	public Camera cam;
	private RaycastHit interactionRaycastHit;
	private string loadPrompt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (cam.transform.position, cam.gameObject.transform.forward, Color.cyan, 2);
		if (Physics.Raycast (cam.transform.position, cam.gameObject.transform.forward, out interactionRaycastHit, 2)) {
			if (interactionRaycastHit.collider.gameObject.tag == "Interaction") {
				loadPrompt = "Press [F] for Interaction";

				//Debug.LogError("InteractionRaycastHit.");
			} else {
				loadPrompt = "";
			}
		} else {
			loadPrompt = "";
		}
	}

	void OnGUI()
	{
		GUI.Label (new Rect (30, Screen.height * .9f, 200, 40), loadPrompt);
	}
}
