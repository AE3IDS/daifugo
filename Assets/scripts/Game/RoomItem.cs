using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RoomItem : MonoBehaviour {

	public GameObject roomName;
	public GameObject rules;
	public GameObject numOfPlayers;


	public void setItemDetails(){


		/* set rules test */

		Text txtRules = rules.GetComponent<Text> ();


		/* set numOfPlayers */

		Text txtNumOfPlayers  = numOfPlayers.GetComponent<Text>();


		/* set room name */

		Text txtName = roomName.GetComponent<Text> ();


	}


	public void joinButtonHandler(){



	}


}
