using UnityEngine;
using System.Collections;

public class r : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D c){
		Debug.Log ("enter collision");
	}

	void OnTriggerEnter2D(Collider2D c){
		Debug.Log ("enter collision1");
	}
}
