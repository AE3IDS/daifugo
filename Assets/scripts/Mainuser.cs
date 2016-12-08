using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Mainuser : UserTable {


	public float CARD_Y = 22.0f;

	public GameObject action;
	public GameObject space;

	private List<GameObject> selectedCards;
	private List<GameObject> cards;

	private bool _turn = false;

	private float CARD_SPACE = 100.0f;
	private float mainCardX = 87.0f;

	public GameObject transporter;


	#region public methods


	public void addCards(int suit, int rank){

		GameObject j = produceCard (
			new Vector2 (168.0f, 250.0f),
			new Vector3 (mainCardX, CARD_Y, 0),
			gameObject,
			suit, rank,
			Vector2.zero, Vector2.zero,
			true);

		cards.Add (j);
		mainCardX += CARD_SPACE;

	}


	public void toggleTurn(){

		_turn = !_turn;

		foreach (GameObject g in cards) {
			g.GetComponent<Button> ().interactable = _turn;
		}

		action.SetActive (_turn);

	}


	public override void endDealt(int[][] cards){

		action.SetActive (false);

		base.calculateX (cards.Length);

		StartCoroutine (displayDealtCoroutine (base.cardX, cards));

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


	#endregion




	#region private methods


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


	void OnCollisionEnter2D(Collision2D c)
	{
		Destroy (c.gameObject);
	}


	private GameObject produceCard(Vector2 size, Vector3 pos, GameObject parent, int suit, int rank, Vector2 anchorMin, Vector2 anchorMax, bool addHandler){

		CardMaker s = new CardMaker ();

		s.setCardInteractable (false);
		s.setCardSize (size);
		s.setCardDetails (suit, rank);

		s.getCard ().transform.SetParent (parent.transform);

		if (addHandler) {
			s.addHandler (delegate { cardHandler (s.getCard ()); });
		}


		if (!anchorMin.Equals(Vector2.zero)) {
			s.setAnchor (anchorMin, anchorMax);
		}

		s.set3DPosition (pos);

		return s.getCard ();
	}


	IEnumerator displayDealtCoroutine(float startX, int[][] cards){

		yield return new WaitForSeconds (0.6f);


		for (int i = 0; i < cards.Length; i++) {

			selectedCards [i].GetComponent<Animator> ().SetBool ("dealt", true);
			yield return new WaitForSeconds (0.5f);
			selectedCards [i].SetActive (false);

			float containerHeight = space.GetComponent<RectTransform> ().sizeDelta.y;

			produceCard (
				new Vector2 (74.0f, containerHeight), 
				new Vector3 (startX, 0, 0), 
				space, cards [i] [0], cards [i] [1], 
				base.minDealtCardAnchor, base.maxDealtCardAnchor,
				false);

			startX += base.DISPLAYDEALTCARD_SPACE;
			yield return new WaitForSeconds (0.75f);
		}

		selectedCards.Clear ();


		Dictionary<string,object> data = new Dictionary<string,object>{
			{"userId",this.userId}
		};

		Transporter t = transporter.GetComponent<Transporter> ();
		t.sendEndDistribute (data);


		yield return null;
	}




	#endregion




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
