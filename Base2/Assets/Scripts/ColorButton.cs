using UnityEngine;
using System.Collections;

public class ColorButton : MonoBehaviour {

	public Material selectedMaterial;
	private Material oldMaterial;
	public Material defaultMaterial;
	private float alive = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider col) {
		
		if (col.tag == "RedButton") {
			col.GetComponent<Renderer> ().material = selectedMaterial;
		} else if (col.tag == "GreenButton") {
			
		} else if (col.tag == "BlueButton") {

		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "RedButton") {
			col.GetComponent<Renderer> ().material = defaultMaterial;
			
			col.transform.parent.gameObject.GetComponent<Animator> ().Play("ButtonPress");
		} else if (col.tag == "GreenButton") {
			
		} else if (col.tag == "BlueButton") {
			
		}
	}
}
