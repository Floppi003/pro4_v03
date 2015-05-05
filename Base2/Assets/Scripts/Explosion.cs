using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public AudioClip explosionSound;
	public GameObject deathParticles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode()
	{
	//	Debug.Log ("Explooooooode!");
		//GameObject.Find ("Player").GetComponent<AudioSource>().clip = explosionSound;
		GameObject.Find ("Player").GetComponent<AudioSource> ().PlayOneShot (explosionSound);
		Instantiate(deathParticles, transform.position, Quaternion.Euler(270,0,0));
		transform.position = new Vector3 (0.0f, -15.0f, 0.0f);
	}
}