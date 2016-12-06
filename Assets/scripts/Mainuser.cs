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

	void cardHandler(GameObject j){

		j.GetComponent<CardScript>().cardClicked = !j.GetComponent<CardScript>().cardClicked;

		bool isClicked = j.GetComponent<CardScript>().cardClicked;
		j.GetComponent<Animator> ().SetBool ("clicked", isClicked);

		if(isClicked){
			selectedCards.Add(j);
		}else{
			selectedCards.Remove(j);
		}

	}


	public void addCards(int suit, int rank){

		CardMaker card = new CardMaker ();

		card.setCardDetails (suit, rank);
		card.setCardInteractable (false);
		card.getCard ().transform.SetParent (transform, true);
		card.set3DPosition(new Vector3 (cardXMain, CARD_Y, 0));
		card.setCardSize(new Vector2(168.0f,250.0f));

		card.addHandler (delegate { cardHandler (card.getCard ()); });

		cards.Add (card.getCard());
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
