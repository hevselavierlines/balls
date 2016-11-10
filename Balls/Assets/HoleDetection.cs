using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HoleDetection : MonoBehaviour {
	public Text holdDetection;
	// Use this for initialization
	void Start () {
		holdDetection.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		holdDetection.text = "Hole detection";
	}
}
