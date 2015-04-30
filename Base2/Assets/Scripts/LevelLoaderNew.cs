using UnityEngine;
using System.Collections;

public class LevelLoaderNew : MonoBehaviour {
	public int levelToLoad;
	public GameObject padlock;
	private string loadPrompt;
	private bool inRange;
	private int unlockedLevel;
	private bool canLoadLevel;

	void Start()
	{
		unlockedLevel = PlayerPrefs.GetInt ("Level Unlocked");
		/*
		if (unlockedLevel == 0) 
		{
			unlockedLevel = 1;
		}
		*/
		canLoadLevel = levelToLoad <= unlockedLevel ? true : false;
		if(!canLoadLevel)
		{
			Instantiate (padlock, new Vector3(transform.position.x - 1f, 1.5f, transform.position.z), Quaternion.identity);
		}
	}

	void Update()
	{
		if (canLoadLevel && inRange && Input.GetButtonDown ("Action")) {
			if (levelToLoad.ToString () == "0") {	//Reset Game
				PlayerPrefs.SetInt("Level Unlocked", 1);
				PlayerPrefs.SetInt ("Current Level", 1);
				Application.LoadLevel("Level 1");
			} else {
				Application.LoadLevel ("Level " + levelToLoad.ToString ()); //load by name instead of id
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		inRange = true;
		if (levelToLoad.ToString () == "0") {
			loadPrompt = "Press [I] to start a new game";
		}else if (canLoadLevel) {
			loadPrompt = "Press [I] to load level " + levelToLoad.ToString ();
		} else {
			loadPrompt = "Level " + levelToLoad.ToString () + " is locked";
		}
	}

	void OnTriggerExit()
	{
		loadPrompt = "";
		inRange = false;
	}

	void OnGUI()
	{
		GUI.Label (new Rect (30, Screen.height * .9f, 200, 40), loadPrompt);
	}
}
