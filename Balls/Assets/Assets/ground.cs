using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {

public Material material;

public float Bounciness;
public float Friction;

	// Use this for initialization
	void Start () {
    }

	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter(Collision col){
		col.rigidbody.AddForce(-col.relativeVelocity*Bounciness);
		col.rigidbody.angularDrag = Friction;
	}
	void OnCollisionStay(Collision col) {
		//col.rigidbody.AddForce(-col.relativeVelocity*Bounciness);
		//col.rigidbody.angularDrag = Friction;	
	}
}
