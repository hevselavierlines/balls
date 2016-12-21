using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class golfclub : MonoBehaviour {

	Vector3 anchor;
	public int moveCoef;
	public int rotateCoef;
	public static bool swingcheck;
	public float force = 30.00f;
	public Vector3 shootDir;
	// Use this for initialization
	void Start () {
		moveCoef = 5;
		rotateCoef = 5;

		}

	// Update is called once per frame
	void Update () {

		/*if(Input.GetKey(KeyCode.Space)){
				transform.Rotate(Vector3.right, Time.deltaTime*rotateCoef);
		}
		if(Input.GetKey(KeyCode.B)){
			transform.Rotate(-Vector3.right, Time.deltaTime*rotateCoef);
		}
		if(Input.GetKey(KeyCode.W)){
			transform.Translate(Vector3.forward * Time.deltaTime*moveCoef, Space.World);

		}if(Input.GetKey(KeyCode.S)){
			transform.Translate(Vector3.back* Time.deltaTime*moveCoef, Space.World);
		}
		if(Input.GetKey(KeyCode.A)){
			transform.Translate(Vector3.left* Time.deltaTime*moveCoef, Space.World);
		}
		if(Input.GetKey(KeyCode.D)){
			transform.Translate(Vector3.right* Time.deltaTime*moveCoef, Space.World);
		}
		if(Input.GetKey(KeyCode.X)){
			transform.Translate(Vector3.up* Time.deltaTime*moveCoef, Space.World);
		}
		if(Input.GetKey(KeyCode.Z)){
			transform.Translate(Vector3.down* Time.deltaTime*moveCoef, Space.World);
		}

		if(Input.GetKey(KeyCode.Q)){
			transform.Rotate(Vector3.up, Time.deltaTime*rotateCoef);
		}
		if(Input.GetKey(KeyCode.E)){
			transform.Rotate(-Vector3.up, Time.deltaTime*rotateCoef);
		}*/

	}
	void OnCollisionEnter(Collision col){
		if (col.collider.name == "Player") {
			Console.WriteLine ("JONAS");
			print (this.name + "col enter" + col.collider.name);
		}
		//Console.Write ("collission enter");
		//print (this.name + "col enter" + col.collider.name);
	}
	void OnCollisionStay(Collision col){
		if(col.collider.gameObject.name == "Player"){
			foreach(ContactPoint contact in col.contacts){
				shootDir = transform.position - contact.point;
				print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
				Debug.DrawRay(contact.point, contact.normal, Color.white);
				col.collider.GetComponent<Rigidbody>().AddForce(this.transform.forward * col.relativeVelocity.magnitude * force);
			}
		}

	}
}
