using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
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
			Instantiate (padlock, new Vector3(transform.position.x + 2f, 0f, transform.position.z), Quaternion.identity);
		}
	}

	void Update()
	{
		if (canLoadLevel && inRange && Input.GetButtonDown("Action")) 
		{
			Application.LoadLevel ("Level " + levelToLoad.ToString ()); //load by name instead of id
		}
	}

	void OnTriggerStay(Collider other)
	{
		inRange = true;
		if (canLoadLevel) {
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
