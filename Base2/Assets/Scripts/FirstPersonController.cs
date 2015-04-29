using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class FirstPersonController : MonoBehaviour {
	public GameManager manager;
	private Vector3 spawn;

	// public vars
	public float mouseSensitivityX = 250;
	public float mouseSensitivityY = 250;
	public float walkSpeed = 6; //movement/walking speed
	public float jumpForce = 260; //jump height/strength
	public float jumpDamping = 3.5f; // reduced movement while jumping
	public LayerMask groundedMask; //mask for raytracing/jumping - reference plane for the raycast#

	// ---- jump width tests
	public float jumpHeight = 0;
	public float jumpWidth = 0;
	private Vector3 jumpStart;
	private Vector3 jumpEnd;
	private Vector3 lastPos = new Vector3(0,0,0);
	private float timePassed = 0;
	public float speed = 0;
	public bool inAir;
	public bool debug = true;
	//

	// audio files
	public AudioClip greenClip1;
	public AudioClip greenClip2;
	public AudioClip greenClip3;
	public AudioClip redClip1;
	public AudioClip redClip2;
	public AudioClip blueClip1;
	public AudioClip blueClip2;

	// audio files bridge and falldown
	public AudioClip bridgeBeforeClip1;
	public AudioClip bridgeBeforeClip2;
	public AudioClip fellofGeneralClip1;
	public AudioClip fellofGeneralAdvancedClip1;
	public AudioClip fellofBridgeClip1;
	public AudioClip fellofBridgeClip2;
	private int fellofClipsPlayed = 0;

	public bool isInFellofZone = false;
	public int fellOfBridgeCounter = 0;

	private float timeSinceLastButtonAudioPlay = 0.0f;


	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;

	
	void Awake() {
		//Cursor.visible = false;
		Screen.lockCursor = true;
		cameraTransform = Camera.main.transform;
	}
	
	void Update() {
	
		timeSinceLastButtonAudioPlay += Time.deltaTime;
		// set dampig dependend if grounded or not
		float damping;
		if (IsGrounded()) {
			damping = 1;
		} else {
			damping = jumpDamping;
		}
		// Look rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;

		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f * damping); //ref allows to modify a global variable

		// Jump
		if (debug && inAir && GetComponent<Rigidbody> ().position.y <= 1.0001f) {
			inAir = false;
			jumpEnd = GetComponent<Rigidbody> ().position; //-----------
			jumpWidth = (jumpEnd - jumpStart).magnitude;
		}

		if (Input.GetButtonDown("Jump")) {
			Debug.Log("Jump!");
			if (IsGrounded()) {
				jumpStart = GetComponent<Rigidbody>().position; //-----------
				inAir = true;
				GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
				Debug.Log("Grounded!");
			}
		}

		//----------------
		if (debug) {
			if (Time.time > 3 && jumpHeight <= GetComponent<Rigidbody> ().position.y - 1) { //-----------
				jumpHeight = GetComponent<Rigidbody> ().position.y - 1; 
			}

			if (timePassed >= 1) {
				speed = (transform.position - lastPos).magnitude / timePassed;
				timePassed = 0;
				lastPos = transform.position;
			}
			timePassed += Time.deltaTime;
		}
		//
	}
	
	bool IsGrounded ()
	{
		//Physics.Raycast(ray, out hit, 1 + .2f, groundedMask
		return (Physics.Raycast (transform.position, - transform.up, 1 + 0.3f, groundedMask)); //letzter Parameter groundedMask
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime; //transform to local space (instead of world space - move on the surface of the sphere)
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);
	}

	public void respawn() {
		Debug.Log ("respawn executed");
		this.transform.position = new Vector3(25.1f, 1.32f, -0.61f);
		this.isInFellofZone = false;
	}

	// play Audio Files 
	public void playGreenSound() {
		if (timeSinceLastButtonAudioPlay < 3.0f) {
			return;
		}

		timeSinceLastButtonAudioPlay = 0.0f;


		Debug.Log ("play Green sound called");
		float random = Random.Range (0.0f, 3.0f);

		if (random < 1.0f) {
			GetComponent<AudioSource> ().PlayOneShot (this.greenClip1);
		} else if (random < 2.0f) {
			GetComponent<AudioSource> ().PlayOneShot (this.greenClip2);
		} else {
			GetComponent<AudioSource> ().PlayOneShot (this.greenClip3);
		}
	}


	public void playRedSound() {
		if (timeSinceLastButtonAudioPlay < 3.0f) {
			return;
		}
		
		timeSinceLastButtonAudioPlay = 0.0f;


		Debug.Log ("play Red sound called");
		float random = Random.Range (0.0f, 2.0f);

		if (random < 1.0f) {
			GetComponent<AudioSource> ().PlayOneShot (this.redClip1);
		} else {
			GetComponent<AudioSource> ().PlayOneShot (this.redClip2);
		}
	}

	public void playBlueSound() {
		if (timeSinceLastButtonAudioPlay < 3.0f) {
			return;
		}
		
		timeSinceLastButtonAudioPlay = 0.0f;


		Debug.Log ("play Blue sound called");
		float random = Random.Range (0.0f, 2.0f);

		if (random < 1.0f) {
			GetComponent<AudioSource> ().PlayOneShot (this.blueClip1);
		} else {
			GetComponent<AudioSource> ().PlayOneShot (this.blueClip2);
		}
	}

	public void playBridgeBeforeSound() {
		// plays bridge before sound
		float random = Random.Range (0.0f, 2.0f);

		if (random < 1.0f) {
			GetComponent<AudioSource>().PlayOneShot (this.bridgeBeforeClip1);
		} else {
			GetComponent<AudioSource>().PlayOneShot (this.bridgeBeforeClip2);
		}
	}

	public void playFellofGeneralSound() {
		// only plays general sound
		float random;

		if (this.fellofClipsPlayed > 2) {
			random = Random.Range (0.0f, 2.0f);
		} else {
			random = Random.Range (0.0f, 0.99f);
		}


		if (random < 1.0f) {
			GetComponent<AudioSource>().PlayOneShot (this.fellofGeneralClip1);
		} else {
			GetComponent<AudioSource>().PlayOneShot (this.fellofGeneralAdvancedClip1);
		}

		this.fellofClipsPlayed++;
	}

	public void playFellofBridgeSound() {
		// plays fellof bridge and fellof general
		float random;
		
		if (this.fellofClipsPlayed > 2) {
			random = Random.Range (0.0f, 4.0f);
		} else {
			random = Random.Range(0.0f, 3.0f);
		}


		if (random < 1.0f) {
			GetComponent<AudioSource>().PlayOneShot (this.fellofBridgeClip1);
		} else if (random < 2.0f) {
			GetComponent<AudioSource>().PlayOneShot (this.fellofGeneralClip1);
		} else if (random < 3.0f) {
			GetComponent<AudioSource>().PlayOneShot (this.fellofBridgeClip2);
		} else {
			GetComponent<AudioSource>().PlayOneShot (this.fellofGeneralAdvancedClip1);
		}

		this.fellofClipsPlayed++;
	}

	public AudioClip fellofBridgeSound() {
		// plays fellof bridge and fellof general
		float random;
		
		if (this.fellofClipsPlayed > 2) {
			random = Random.Range (0.0f, 4.0f);
		} else {
			random = Random.Range(0.0f, 3.0f);
		}
		

		this.fellofClipsPlayed++;

		if (random < 1.0f) {
			return this.fellofGeneralClip1;
		} else if (random < 2.0f) {
			return this.fellofBridgeClip1;
		} else if (random < 3.0f) {
			return this.fellofBridgeClip2;
		} else {
			return this.fellofGeneralAdvancedClip1;
		}

		return null;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Enemy")
		{
			Die ();
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Enemy")
		{
			Die ();
		}
		if (other.transform.tag == "Goal")
		{
			manager.CompleteLevel();
		}
	}
	
	void Die()
	{		
		transform.position = spawn;
	}
}