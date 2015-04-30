using UnityEngine;
using System.Collections;

public class LevelLoaderNew : MonoBehaviour {
	public int levelToLoad;
	public GameObject padlock;
	private string loadPrompt;
	//private bool inRange;
	private int unlockedLevel;
	private bool canLoadLevel = true;

	void Start()
	{
		unlockedLevel = PlayerPrefs.GetInt ("Level Unlocked");
		PlayerPrefs.SetInt("Chosen Level", unlockedLevel);
		/*
		if (unlockedLevel == 0) 
		{
			unlockedLevel = 1;
		}
		*/

		canLoadLevel = levelToLoad <= unlockedLevel ? true : false;
		if(!canLoadLevel)
		{
			Instantiate (padlock, new Vector3(transform.position.x - 0.7f, 1.5f, transform.position.z), Quaternion.identity);
		}
	}

	public void LevelButtonPressed()
	{
		if (canLoadLevel && Input.GetButtonDown ("ButtonPressed")) {
			if (levelToLoad.ToString () == "0") {	//Reset Game
				PlayerPrefs.SetInt("Level Unlocked", 1);
				PlayerPrefs.SetInt ("Current Level", 1);
				Application.LoadLevel("Level 1");
			} else {
				PlayerPrefs.SetInt("Chosen Level", levelToLoad);
		//		Application.LoadLevel ("Level " + levelToLoad.ToString ()); //load by name instead of id
			}

			Debug.Log("Button pressed.");
			playAnimation();
			Debug.Log("My name is " + levelToLoad.ToString ());
		}
	}
	/*
	void OnTriggerStay(Collider other)
	{
		inRange = true;
		if (levelToLoad.ToString () == "0") {
			loadPrompt = "Press [F] to start a new game";
		}else if (canLoadLevel) {
			loadPrompt = "Press [F] to load level " + levelToLoad.ToString ();
		} else {
			loadPrompt = "Level " + levelToLoad.ToString () + " is locked";
		}
	}
*/
	public string GetText(){
		string levelText = "";
		if (levelToLoad.ToString () == "0") {
			levelText = "Press [F] to start a new game";
		}else if (canLoadLevel) {
			levelText = "Press [F] to load level " + levelToLoad.ToString ();
		} else {
			levelText = "Level " + levelToLoad.ToString () + " is locked";
		}
		return levelText;
	}
	/*
	void OnTriggerExit()
	{
		loadPrompt = "";
		inRange = false;
	}

	void OnGUI()
	{
		GUI.Label (new Rect (30, Screen.height * .9f, 200, 40), loadPrompt);
	}
*/

	void playAnimation() {
		GetComponent<Animator> ().Play("Push");
	}
}
