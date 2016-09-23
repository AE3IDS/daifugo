using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class avatars : MonoBehaviour {

	public GameObject activeChoice;

	// Use this for initialization
	void Start () {
		Outline x = activeChoice.AddComponent<Outline> ();
		x.effectColor = new Color (0.78f, 0.78f, 0.78f, 1.0f);
		x.effectDistance = new Vector2 (8f, 8f);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	 * Avatar Image onclick handler
	 */

	public void avatarClick(GameObject b){

		activeChoice.GetComponent<Outline> ().effectDistance = new Vector2 (0.0f, 0.0f);

		if (b.GetComponent<Outline> () == null) {
			Outline x = b.AddComponent<Outline> ();
			x.effectColor = new Color (0.78f, 0.78f, 0.78f, 1.0f);
			x.effectDistance = new Vector2 (8f, 8f);
		} else {
			b.GetComponent<Outline> ().effectDistance = new Vector2 (8f, 8f);
		}

		activeChoice = b;
	}

}
