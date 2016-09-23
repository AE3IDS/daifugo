using UnityEngine;
using System.Collections;

public class CardsContainer : MonoBehaviour {

	// Use this for initialization

	public GameObject overlay;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void hideCards(){
		gameObject.GetComponent<Animator> ().SetBool ("show", false);
		Invoke ("hideOverlay", 1.8f);
	}

	public void hideOverlay(){
		overlay.SetActive (false);
	}

}
