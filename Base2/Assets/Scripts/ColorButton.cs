using UnityEngine;
using System.Collections;

public class ColorButton : MonoBehaviour {

	public Material selectedMaterial;
	private Material oldMaterial;
	public Material defaultMaterial;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision other){
		/*Material mat_button = Instantiate(Resources.Load("Button", typeof(Material))) as Material;
		Material mat_button1 = Instantiate(Resources.Load("Button1", typeof(Material))) as Material;

		if (other.gameObject.tag == "RedButton") {
			Debug.LogError ("Red Button!!!!!!!!");
		
			other.gameObject.GetComponent<Renderer>().material = mat_button;

		} else if (other.gameObject.tag == "GreenButton") {
			Debug.LogError ("Green Button!!!!!!!!");
			
			other.gameObject.GetComponent<Renderer>().material = mat_button;

		} else if (other.gameObject.tag == "BlueButton") {
			Debug.LogError ("Blue Button!!!!!!!!");
			
			other.gameObject.GetComponent<Renderer>().material = mat_button;
		}*/
		Debug.LogError ("OnCollisionStay");
		//other.gameObject.GetComponent<AudioSource> ().PlayOneShot (other.gameObject.GetComponent<FirstPersonController>().helloClip);
		
	}

	void OnTriggerStay(Collider other) {
		//Material mat_button = Instantiate(Resources.Load("Button", typeof(Material))) as Material;
		//Material mat_button1 = Instantiate(Resources.Load("Button1", typeof(Material))) as Material;
		
		if (other.gameObject.tag == "RedButton") {
			//Debug.LogError ("Red Button!!!!!!!!");
//			other.gameObject.GetComponent<Renderer>().material = mat_button;
			
		} else if (other.gameObject.tag == "GreenButton") {
			//Debug.LogError ("Green Button!!!!!!!!");
			
//			other.gameObject.GetComponent<Renderer>().material = mat_button;
			
		} else if (other.gameObject.tag == "BlueButton") {
			//Debug.LogError ("Blue Button!!!!!!!!");
			
//			other.gameObject.GetComponent<Renderer>().material = mat_button;
		}
		//other.material = new PhysicMaterial("test");
		if (other.tag == "RedButton") {
			if (other.GetComponent<Renderer> ().material != oldMaterial) {
				oldMaterial = other.GetComponent<Renderer> ().material;
			}
			other.GetComponent<Renderer> ().material = selectedMaterial;
		}


		//Debug.Log ("OurOnTriggerStay");	
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "RedButton") {
			col.GetComponent<Renderer> ().material = defaultMaterial;
		}
	}
}
