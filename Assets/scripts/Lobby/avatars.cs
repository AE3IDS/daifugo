using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class avatars : MonoBehaviour {

	public GameObject activeChoice;
	private int _selectedIndex = 0;
	public int selectedButton { get { return _selectedIndex; } }

	// Use this for initialization
	void Start () {
		addOutLine (activeChoice);	
	}

	void addOutLine(GameObject b){
		Outline x = b.AddComponent<Outline> ();
		x.effectColor = new Color (0.78f, 0.78f, 0.78f, 1.0f);
		x.effectDistance = new Vector2 (8f, 8f);
	}


	/*
	 * Avatar Image onclick handler
	 */

	public void avatarClick(GameObject b){


		_selectedIndex = b.transform.GetSiblingIndex ();


		activeChoice.GetComponent<Outline> ().effectDistance = new Vector2 (0.0f, 0.0f);

		if (b.GetComponent<Outline> () == null) {
			addOutLine (b);
		} else {
			b.GetComponent<Outline> ().effectDistance = new Vector2 (8f, 8f);
		}

		activeChoice = b;
	}

	public int getAvatarSelection()
	{
		return this._selectedIndex;
	}


}
