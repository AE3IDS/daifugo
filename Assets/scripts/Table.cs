using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {


	private const string tablePlayerTag = "tablePlayer";
	public GameObject overlay;
	public GameObject userActions;
	public GameObject user;
	public GameObject cards;

	public GameObject f;
	private bool test =false;

	// Use this for initialization



	void Awake(){

	}



	void Start () {

		addUser("asdfs",1);
		addUser ("asdfs", 5);
	}

	public void distribute(){
		f.GetComponent<Animator> ().SetBool ("distribute", true);
	}

	public void showCardUser(){


		// hide all user action buttons
		int childCount = userActions.transform.childCount;

		 for (int m = 0; m < childCount; m++) {
			GameObject child = userActions.transform.GetChild (m).gameObject;
			child.GetComponent<Animator> ().SetBool ("showCard", true);
		}


		Invoke ("show", 1.3f);


	}

	private void show(){

		overlay.SetActive (true);
		overlay.GetComponent<OverlayScript> ().toggleCards ();
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
	 * Start Distribute the Cards
	 * 
	 */

	public void distribute1(){

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

			if (childObject.tag == tablePlayerTag) {
				
				t = childObject.GetComponent<UserTable> ();

				if (!t.spaceOccupied) { 
					emptySpace = childObject;
					break;
				}

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
