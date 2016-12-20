using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {


public int bounciness;
public float Friction;

	// Use this for initialization
	void Start () {
		bounciness = 5;
		Friction = 0.3f;
	}
	void OnCollisionEnter(Collision col){
		col.rigidbody.AddForce (-col.relativeVelocity * bounciness);
		col.rigidbody.angularDrag = Friction;
	}
	void OnCollisionStay(Collision col){
		col.rigidbody.angularDrag = Friction;
	}
}
