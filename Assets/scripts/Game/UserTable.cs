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

	protected readonly Vector2 minDealtCardAnchor = new Vector2 (0.5f, 0.5f);
	protected readonly Vector2 maxDealtCardAnchor = new Vector2(0.5f,0.5f);


	/* private variables */


	private const float CARD_Y = 21.0f;
	private bool turn = false;

	private string _userId;
	private int numOfCards = 0;
	public string userId { get { return this._userId; } set { this._userId = value; } }

	private bool _isOccupied = false;
	public bool spaceOccupied { get { return this._isOccupied; } set {this._isOccupied = value; }}


	/*
	 * 
	 * spawn the correct avatar based on the photoId
	 * 
	 */ 

	public void addPhoto(int photoId){

		string imageName = null;

		if (photoId == 0) {
			
			imageName = "dino";

		} else if (photoId == 1) {

			imageName = "minion";

		} else if (photoId == 2) {

			imageName = "dory";

		} else if (photoId == 3) {

			imageName = "monster";

		} else if (photoId == 4) {

			imageName = "anger";

		} else if (photoId == 5) {

			imageName = "baymax";

		} else if (photoId == 6) {

			imageName = "dory";

		}
			
		Texture2D avaTex = Resources.Load ("images/avatar/"+imageName, typeof(Texture2D)) as Texture2D;

		avatar.GetComponent<Image> ().sprite = Sprite.Create (avaTex, new Rect (0, 0, avaTex.width, avaTex.height), new Vector2 (0.0f, 0.0f));


	}

	private void addCards(){

		numOfCards++;

		Text t = numOfCardsLabel.GetComponent<Text> ();

		char[] space = new char[]{ '-',' ' };
		string[] s = t.text.Split (space, 2);

		t.text = numOfCards.ToString () + " " + s [1];

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

	public void toggleTurn(){

		turn = !turn;
		turnArrow.SetActive (turn);

	}

}
