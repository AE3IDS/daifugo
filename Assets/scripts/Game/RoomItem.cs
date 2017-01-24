using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RoomItem : MonoBehaviour {

	public GameObject roomName;
	public GameObject rules;
	public GameObject numOfPlayers;


	public void setItemDetails(string rulesStr, string name, string num){


		/* set rules test */

		Text txtRules = rules.GetComponent<Text> ();

		txtRules.text = txtRules.text + rulesStr;



		/* set numOfPlayers */

		Text txtNumOfPlayers  = numOfPlayers.GetComponent<Text>();

		txtNumOfPlayers.text = txtNumOfPlayers.text + num;


		/* set room name */

		Text txtName = roomName.GetComponent<Text> ();

		txtName.text = txtName.text + name;


	}


	public void joinButtonHandler(){



	}


}
