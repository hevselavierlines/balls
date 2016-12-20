using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public int bounciness;
	public float Friction;
	private Rigidbody rb;
	// Use this for initializationpubli
	void Start () {
		bounciness = 10;
		rb= GetComponent<Rigidbody>();
		rb.angularDrag = 0.2f;

	}

	// Update is called once per frame
	void Update () {
		
	if(rb.velocity.magnitude < 0.5){
		rb.velocity = new Vector3(0,0,0);
		}
	}
	void OnCollisionEnter(Collision col){
		if(col.collider.gameObject.name == "Ground"){
			foreach(ContactPoint contact in col.contacts){
				Debug.DrawRay(contact.point, contact.normal, Color.white);
				rb.AddForce(contact.normal * col.relativeVelocity.magnitude * bounciness);
			}
		}
	}
}
