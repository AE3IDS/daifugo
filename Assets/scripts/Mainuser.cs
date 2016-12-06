using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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

		j.GetComponent<CardScript> ().setCardDetails (suit, rank);
		j.GetComponent<Button> ().interactable = false;
		j.transform.SetParent (transform,true);
		j.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (cardXMain, CARD_Y, 0);

		j.GetComponent<Button> ().onClick.AddListener (delegate {

			j.GetComponent<CardScript>().cardClicked = !j.GetComponent<CardScript>().cardClicked;

			bool isClicked = j.GetComponent<CardScript>().cardClicked;
			j.GetComponent<Animator> ().SetBool ("clicked", isClicked);

			if(isClicked){
				selectedCards.Add(j);
			}else{
				selectedCards.Remove(j);
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

	public void endDealt(){

		action.SetActive (false);

		for (int i =0;i < transform.childCount;i++) {
			
			GameObject child = transform.GetChild (i).gameObject;
			if (selectedCards.Contains (child)) {
				child.SetActive (false);
			}
	
		}

		selectedCards.Clear ();

	}
		

	/* Deal Card button handler */

	public void dealCard(){

		SocketConnection _socket = (GameObject.FindWithTag ("socket")).GetComponent<SocketConnection> ();
		List<Dictionary<string,int>> s = new List<Dictionary<string, int>> ();


		foreach (GameObject card in selectedCards) {

			CardScript cardScript = card.GetComponent<CardScript> ();
		
			Dictionary<string,int> cardDetails = new Dictionary<string,int> {
				{ "suit",cardScript.cardSuit },
				{ "kind",cardScript.cardRank }
			};

			s.Add (cardDetails);
		}

		_socket.sendSelectedCards (this.userId, s.ToArray());
	}

	/* testing code */

	public void init(){

		cards = new List<GameObject> ();
		selectedCards = new List<GameObject> ();

	}

	public void addSelected(GameObject j){

		selectedCards.Add (j);
	
	}

	public List<GameObject> getCards(){

		return cards;

	}

	public List<GameObject> getSelected(){

		return selectedCards;
	}

}
