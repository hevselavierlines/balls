using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	private long time;
	private Vector3 startPos;
	private int notMoving = -1;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		time = 0;
		startPos = transform.position;
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
			}
		}
		if (notMoving == -1) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			if (Input.GetKey ("up")) {
				time++;
			} else if (Input.GetKeyUp (KeyCode.UpArrow)) {
				Vector3 movement = new Vector3 (0.0f, 0.0f, time * speed);
				rb.AddForce (movement);
				time = 0;
				notMoving = 0;
			}
		}

		if (transform.position.y < 0.0) {
			reset ();
		}
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertial = Input.GetAxis ("Vertical");

		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertial);
		//rb.AddForce (movement * speed);
	}

	void reset() {
		transform.position = startPos;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero; 
		time = 0;
		notMoving = -1;
	}
}
