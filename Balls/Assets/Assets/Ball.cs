using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public int bounciness;
	public float angularDrag;
	public Rigidbody rb;

	private Vector3 golfclubOffset;
	public float speed;
	public GameObject golfclub;
	public GolfClubControl gc;

	private Vector3 startPos;
	private int notMoving = -1;


	void Start() {
		rb = GetComponent<Rigidbody> ();
		startPos = transform.position;
		golfclubOffset = golfclub.transform.position - transform.position;

		bounciness = 10;
		rb= GetComponent<Rigidbody>();
		angularDrag = 1.0f;
	}

	void ballStopped() {
		gc.clubAppear ();
	}

	public bool IsBallRolling() {
		return notMoving != -1;
	}

	public void HitBall() {
		notMoving = 0;
	}


	void FixedUpdate() {
		if (Input.GetKey (KeyCode.R)) {
			reset ();
		}
		if (notMoving != -1 && Mathf.Abs(rb.velocity.x) < 0.15 && Mathf.Abs(rb.velocity.z) < 0.15 
			&& Mathf.Abs(rb.velocity.y) < 0.05) {
			notMoving++;
			print ("Not moving " + notMoving);
			if (notMoving > 60) {
				notMoving = -1;
				ballStopped ();
			}
		}
		if (notMoving == -1) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		if (transform.position.y < 0.0) {
			reset ();
			ballStopped ();
		}
	}

	void reset() {
		transform.position = startPos;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero; 
		notMoving = -1;
		golfclub.transform.eulerAngles = new Vector3 (25f, 180f, 0.0f);
	}

	// Update is called once per frame
	void Update () {
		golfclub.transform.position = transform.position + golfclubOffset;
        angularDrag = 1.0f;

	}
	void OnCollisionEnter(Collision col){
		if(col.collider.gameObject.tag == "environment"){
			foreach(ContactPoint contact in col.contacts){
				Debug.DrawRay(contact.point, contact.normal, Color.white);
				rb.AddForce(contact.normal * col.relativeVelocity.magnitude * bounciness);
			}
		}
	}
	void OnCollisionStay(Collision col){
		if(col.collider.gameObject.tag == "environment"){
			rb.angularDrag = angularDrag;
		}
		if(rb.velocity.magnitude < 0.2){
			rb.velocity = new Vector3(0,0,0);
		}
	}
}
