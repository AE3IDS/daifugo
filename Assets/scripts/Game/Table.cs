using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {


	private const string tablePlayerTag = "tablePlayer";

	public GameObject user;
	public GameObject card;
	string[] cardStates = new string[] {"distributeUp","distributeRight","distributeDown","distributeLeft"};
	private GameObject[] otherPlayers = new GameObject[3];
	private UserTable prevTurn = null;
	private int[,] cards;



	void Start(){

		int childCount = gameObject.transform.childCount;
		int counter = 0;
	
		for(int i = 0; i < childCount;i++){

			GameObject childObject = gameObject.transform.GetChild (i).gameObject;

			if (childObject.tag == tablePlayerTag) {
				otherPlayers [counter++] = childObject;
			}
		}

	}

	public void card1(int[,] m){

		cards = m;
	}


	/*   Switch the players turn   */

	private void switchTurn(string id){


		/* Toggle the turn of the last player */

		if (prevTurn != null) {
			prevTurn.toggleTurn ();
		}


		/* Find the player for next turn based on id */

		bool found = false;

		for (int i = 0; i < otherPlayers.Length; i++) {
			UserTable t = otherPlayers [i].GetComponent<UserTable> ();
			if (t.userId == id) {
				t.toggleTurn ();
				prevTurn = t;
				found = true;
				break;
			}
		}

		/* found == false, main players turn */ 

		if (!found) {
			user.GetComponent<Mainuser> ().toggleTurn ();
			prevTurn = user.GetComponent<Mainuser> ();

		}
			
	}



	/*  a coroutine to distributes the cards */

	IEnumerator initializeCoroutine(string turnId, int turnPhotoId){

		/* distribute the cards */

		int i = 0;
		int cardCounter = 0;
		int y = 3;

		yield return new WaitForSeconds (0.8f);

		while (i != 52) {

			spawn (cardStates[(i)%4]);

			if (i == y) {
				user.GetComponent<Mainuser> ().addCards (cards[cardCounter,0],cards[cardCounter,1]);
				cardCounter++;
				y += 4;
			}

			yield return new WaitForSeconds (0.9f);
			i++;
				
		}
		card.SetActive (false);

		/* assign the turn */

		switchTurn (turnId);
	}



	/* instantiates new card prefab for distribution */

	GameObject spawn(string anim){

		GameObject d = Instantiate (card, card.GetComponent<RectTransform>().anchoredPosition3D, Quaternion.identity) as GameObject;

		d.transform.SetParent (transform);

		d.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		d.transform.SetSiblingIndex (2);
		d.GetComponent<Animator> ().SetBool (anim, true);

		return d;
	}


	/* 
	 * perform initialization of the game table;  
	 * distribute and assign the correct turn
	 */
		
	public void initializeTable(string turnId, int turnPhotoId){

		card.SetActive (true);
		StartCoroutine (initializeCoroutine(turnId,turnPhotoId));

	}
		

	/* 	add userId to the main player 	*/

	public void addOwnerId(string id){

		user.GetComponent<Mainuser> ().userId = id;

	}


	/*  add new player to the first vacant seat  */ 

	public void addUser(string userid, int photoId){

		foreach (GameObject player in otherPlayers) {

			UserTable emptyUser = player.GetComponent<UserTable>();

			if (!emptyUser.spaceOccupied) {
				emptyUser.userId = userid;
				emptyUser.addPhoto (photoId);
				emptyUser.spaceOccupied = true;
				break;
			}
		}


	}


}
