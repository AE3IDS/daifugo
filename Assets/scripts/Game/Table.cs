using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {


	private const string tablePlayerTag = "tablePlayer";

	public GameObject user;
	public GameObject card;
	string[] cardStates = new string[] {"distributeUp","distributeRight","distributeDown","distributeLeft"};


	/*
	 * 
	 * Coroutine to distribute cards
	 * 
	 */


	IEnumerator distributeCardCoroutine(){

		int i = 0;
		while (i != 53) {

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


}
