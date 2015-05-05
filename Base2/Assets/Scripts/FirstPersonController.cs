using UnityEngine;
using UnityEngine.UI;
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

	private float timeSinceLastButtonAudioPlay = 0.0f;

	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	
	
	void Start() { //Awake
		Cursor.visible = false;
		Screen.lockCursor = true;
		cameraTransform = Camera.main.transform;
		spawn = transform.position;
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

			if (IsGrounded()) {
				jumpStart = GetComponent<Rigidbody>().position; //-----------
				inAir = true;
				GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);

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
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime; //transform to local space (instead of world space - move on the surface of the sphere)
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);
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

	public void Die()
	{		
		transform.position = spawn;
	}
	
	public void ChangeMouseSensitivity(float sensitivity){
		mouseSensitivityX = sensitivity;
		mouseSensitivityY = sensitivity;
		string sensitivityString = sensitivity.ToString ();
	}
}