using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserTable : MonoBehaviour {

	public GameObject avatar;
	public GameObject numOfCardsLabel;
	public GameObject turnArrow;


	/* Protected variables */


	protected readonly float DISPLAYDEALTCARD_SPACE = 45.0f;
	protected float cardX = 0;
	protected readonly float CARDSPACE = 74.0f;
	protected bool _turn = false;

	protected readonly Vector2 minDealtCardAnchor = new Vector2 (0.5f, 0.5f);
	protected readonly Vector2 maxDealtCardAnchor = new Vector2(0.5f,0.5f);


	/* private variables */


	private const float CARD_Y = 21.0f;

	private string _userId;

	private int _numOfCards = 0;
	public string userId { 
		get { return this._userId; } 
		set { this._userId = value; } 
	}

	private bool _isOccupied = false;

	public bool spaceOccupied { 
		get { return this._isOccupied; } 
		set {this._isOccupied = value; }
	}


	/*
	 * 
	 * add avatar
	 * 
	 */ 

	public void addPhoto(int photoId){
		
		avatar.GetComponent<Image> ().sprite = Util.getAvatarForId (photoId);

	}


	private void addCards(){

		_numOfCards++;

		Text t = numOfCardsLabel.GetComponent<Text> ();

		char[] space = new char[]{ '-',' ' };
		string[] s = t.text.Split (space, 2);

		t.text = _numOfCards.ToString () + " " + s [1];

	}


	public virtual void endDealt(int[][] cards){



	}


	public void calculateX(int length){

		int numOfLeftSideCards = length / 2;

		if (length % 2 != 0) {
			
			this.cardX = numOfLeftSideCards * DISPLAYDEALTCARD_SPACE * -1;

		} else {

			float m = ((CARDSPACE / 2) / 2);
			float h = (numOfLeftSideCards - 1) * DISPLAYDEALTCARD_SPACE;
			this.cardX = (h + m) * -1;

		}

	}

	void OnCollisionEnter2D(Collision2D c){
		
		Destroy (c.gameObject);
		addCards ();

	}


	public bool getTurn(){

		return this._turn;

	}


	public void toggleTurn(){

		_turn = !_turn;
//		turnArrow.SetActive (_turn);

	}

}
