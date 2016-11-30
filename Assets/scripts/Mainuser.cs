using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Mainuser : UserTable {

	public float CARD_SPACE = 100.0f;
	public float CARD_Y = 22.0f;
	public float cardXMain = 87.0f;
	public GameObject card;
	public GameObject action;

	private int cardCounter = 0;
	private List<GameObject> selectedCards;
	private List<GameObject> cards;
	private bool _turn = false;

	void Start(){

		cards = new List<GameObject> ();
		selectedCards = new List<GameObject> ();


	}

	public void addCards(int suit, int rank){

		GameObject j = Instantiate (card, new Vector3(cardXMain,CARD_Y,0), Quaternion.identity) as GameObject;


		j.GetComponent<Button> ().interactable = false;


		// Set the rank & suit

		j.GetComponent<CardScript> ().setCardDetails (suit, rank);
		j.transform.SetParent (transform,true);


		// Set the x & y coordinate

		j.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (cardXMain, CARD_Y, 0);


		// add onclick handler to the card

		j.GetComponent<Button> ().onClick.AddListener (delegate {

			j.GetComponent<CardScript>().cardClicked = !j.GetComponent<CardScript>().cardClicked;
			j.GetComponent<Animator> ().SetBool ("clicked", j.GetComponent<CardScript>().cardClicked);


			if(j.GetComponent<CardScript>().cardClicked){

				selectedCards.Add(j);
				j.GetComponent<CardScript> ().cardIndex = ++cardCounter;
				Debug.Log(j.GetComponent<CardScript> ().cardIndex);

			}else{
				
				Debug.Log(j.GetComponent<CardScript> ().cardRank);

				bool success = selectedCards.Remove(j);

				if(success== false){
					Debug.Log("error remove");
				}
					
				j.GetComponent<CardScript> ().cardIndex = -1;
				cardCounter--;
			}

		});
			
		cards.Add (j);
		cardXMain += CARD_SPACE;
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		Destroy (c.gameObject);
	}


	public void toggleTurn(){

		_turn = !_turn;

		foreach (GameObject g in cards) {
			g.GetComponent<Button> ().interactable = _turn;
		}

		action.SetActive (_turn);

	}

	/*
	 * 
	 * hide user button
	 * 
	 */ 

//	public void showCardUser(){
//
//		// hide all user action buttons
//		int childCount = userActions.transform.childCount;
//
//		for (int m = 0; m < childCount; m++) {
//			GameObject child = userActions.transform.GetChild (m).gameObject;
//			child.GetComponent<Animator> ().SetBool ("showCard", true);
//		}
//
//	}

}
