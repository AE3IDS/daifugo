using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {


	private const string tablePlayerTag = "tablePlayer";

	public GameObject userActions;
	public GameObject user;
	public GameObject card;



	/*
	 * 
	 * Start Distribute the Cards
	 * 
	 */

	public void distribute(){

		card.SetActive (true);
	}



	/*
	 * 
	 * hide user button
	 * 
	 */ 

	public void showCardUser(){

		// hide all user action buttons
		int childCount = userActions.transform.childCount;

		 for (int m = 0; m < childCount; m++) {
			GameObject child = userActions.transform.GetChild (m).gameObject;
			child.GetComponent<Animator> ().SetBool ("showCard", true);
		}
			
	}


	/*
	 * 
	 * add user Id to owner
	 * 
	 */

	public void addOwnerId(string id){

		user.GetComponent<UserTable> ().userId = id;

	}

		
	/*
	 * 
	 * Get the first vacant seat on the table
	 * 
	 */ 

	public void addUser(string userid, int photoId){

		int childCount = gameObject.transform.childCount;
		GameObject emptySpace = null;
		UserTable t = null;

		for(int i =0 ; i < childCount;i++){

			GameObject childObject = gameObject.transform.GetChild (i).gameObject;
			t = childObject.GetComponent<UserTable> ();

			if (childObject.tag == tablePlayerTag && !t.spaceOccupied) {
				
				emptySpace = childObject;
				break;

			}
		}

		t.userId = userid;
		t.addPhoto (photoId);
		t.spaceOccupied = true;
	}

	// Update is called once per frame

	void Update () {
	
	}
}
