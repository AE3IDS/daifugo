using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {


	private const string tablePlayerTag = "tablePlayer";

	public GameObject user;
	public GameObject card;
	string[] cardStates = new string[] {"distributeUp","distributeRight","distributeDown","distributeLeft"};
	private GameObject[] otherPlayers = new GameObject[3];
	private UserTable prevTurn = null;



	void Start(){

		int childCount = gameObject.transform.childCount;
	
		for(int i =0 ; i < childCount;i++){

			GameObject childObject = gameObject.transform.GetChild (i).gameObject;

			if (childObject.tag == tablePlayerTag) {
				otherPlayers [i] = childObject;
			}
		}
	}


	/*
	 * 
	 * Switch the players turn
	 * 
	 */

	public void switchTurn(string id){

		if (prevTurn != null) {
			prevTurn.toggleTurn ();
		}

		bool found = false;

		for (int i = 0; i < otherPlayers.Length; i++) {
			UserTable t = otherPlayers [i].GetComponent<UserTable>();
			if (t.userId == id) {
				t.toggleTurn ();
				prevTurn = t;
				found = true;
			}
		}

		if (!found) {

			user.GetComponent<Mainuser> ().toggleTurn ();
			prevTurn = user.GetComponent<Mainuser> ();

		}


	}



	/*
	 * 
	 * Coroutine to distribute cards
	 * 
	 */


	IEnumerator distributeCardCoroutine(){

		int i = 0;

		yield return new WaitForSeconds (0.8f);

		while (i != 52) {

			GameObject f = spawn (cardStates[(i)%4]);

//			while(f.GetComponent<Animator> ().IsInTransition (0)) {
//				yield return null;
//			}

			yield return new WaitForSeconds (0.9f);
			i++;
		}
		card.SetActive (false);
	}


	/*
	 * 
	 * Start Distribute the Cards
	 * 
	 */

	public void distribute(){

		card.SetActive (true);
		StartCoroutine ("distributeCardCoroutine");

	}
		

	/*
	 * 
	 * add user Id to owner
	 * 
	 */

	public void addOwnerId(string id){

		user.GetComponent<Mainuser> ().userId = id;

	}

	GameObject spawn(string anim){

		GameObject d = Instantiate (card, card.GetComponent<RectTransform>().anchoredPosition3D, Quaternion.identity) as GameObject;
		d.transform.SetParent (transform);
		d.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		d.transform.SetSiblingIndex (2);
		d.GetComponent<Animator> ().SetBool (anim, true);

		return d;
	}

		
	/*
	 * 
	 * Get the first vacant seat on the table
	 * 
	 */ 

	public void addUser(string userid, int photoId){

		for(int i =0 ; i < otherPlayers.Length;i++){

			UserTable emptyUser = otherPlayers [i].GetComponent<UserTable>();

			if (!emptyUser.spaceOccupied) {
				
				emptyUser.userId = userid;
				emptyUser.addPhoto (photoId);
				emptyUser.spaceOccupied = true;

				break;
			}
		}
	}


}
