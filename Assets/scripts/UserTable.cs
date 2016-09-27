using UnityEngine;
using System.Collections;

public class UserTable : MonoBehaviour {

	public GameObject avatar;
	public GameObject cardBack;

	/* Private variables */

	private const float CARD_SPACE = 28.0f;
	private const float CARD_Y = 21.0f;
	private float cardX = -146.0f;

	private string _userId;
	public string userId { get { return this._userId; } set { this._userId = value; } }


	private bool _isOccupied;

	void Start () {
		_isOccupied = false;
	}

	void Update () {
	
	}

	public bool getIsOccupied(){
		return this._isOccupied;
	}


	public void addPhoto(int photoId){

	}

	private void addCards(){
		
		GameObject card = Instantiate (cardBack,Vector3.zero, gameObject.transform.rotation) as GameObject;
		card.transform.SetParent (gameObject.transform, false);
		card.GetComponent<Animator> ().SetBool ("showCard", true);


		RectTransform m = card.GetComponent<RectTransform> ();
		m.anchoredPosition = new Vector2 (cardX, CARD_Y);

		cardX = cardX + CARD_SPACE;
	}

	void OnCollisionEnter2D(Collision2D c){
		Debug.Log ("collision");

		addCards ();

	}

}
