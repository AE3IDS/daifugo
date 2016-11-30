using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {

	int _index;
	public int cardIndex{

		get {
			return this._index;
		}

		set{
			this._index = value;
		}

	}

	bool _isClicked = false;
	public bool cardClicked{

		get{
			return this._isClicked;
		}

		set{

			this._isClicked = value;
		}

	}




	int _suit;
	public int cardSuit	{

		get{

			return this._suit;
		}
	}
		
	int _rank;
	public int cardRank {

		get {

			return this._rank;

		}
	}

	public void setCardDetails(int suit, int rank){

		this._suit = suit;
		this._rank = rank;

		CardSpawner s = new CardSpawner ();
		gameObject.GetComponent<Image>().sprite = s.getSprite (suit, rank);

	}

}
