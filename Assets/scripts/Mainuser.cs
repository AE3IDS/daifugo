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
	public GameObject space;

	private Vector2 minAnchor = new Vector2 (0.5f, 0.5f);
	private Vector2 maxAnchor = new Vector2 (0.5f, 0.5f);

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

	IEnumerator addCoroutine(float startX, int[][] cards){

		yield return new WaitForSeconds (0.6f);

		GameObject[] selectedCardsAr = selectedCards.ToArray ();

		for (int i = 0; i < cards.Length; i++) {

			selectedCards [i].GetComponent<Animator> ().SetBool ("dealt", true);
			yield return new WaitForSeconds (0.5f);
			selectedCards [i].SetActive (false);


			GameObject j = (GameObject) Instantiate (card, new Vector3 (startX, 0, 0), Quaternion.identity);

			RectTransform cardRect = j.GetComponent<RectTransform> ();

			j.transform.SetParent (space.transform,true);
			j.GetComponent<Button> ().interactable = false;


			/* set size of the card */

			float containerHeight = space.GetComponent<RectTransform> ().sizeDelta.y;
			cardRect.sizeDelta = new Vector2 (74.0f, containerHeight);


			j.GetComponent<CardScript> ().setCardDetails (cards[i][0], cards[i][1]);
			cardRect.anchorMin = minAnchor;
			cardRect.anchorMax = maxAnchor;

			cardRect.anchoredPosition3D = new Vector3 (startX, 0, 0);

			startX += base.CARD_SPACE;

			yield return new WaitForSeconds (0.9f);
		}

		selectedCards.Clear ();
		yield return null;
	}

	public override void endDealt(int[][] cards){

		action.SetActive (false);

		base.calculateStartX (cards.Length);

		StartCoroutine (addCoroutine (base.cardX, cards));

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
