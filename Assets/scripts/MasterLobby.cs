using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class MasterLobby : MonoBehaviour,SocketConnectionInterface{

	public Button ruleButton, avatarButton;
	protected ColorBlock _selectedColor, _unselectedColor;
	public GameObject avatarContainer, rulesContainer, socket;

	private SocketConnection _sock;
	private Button _activeButton;
	private string _tempId;



	void Start () {
			
		_sock = socket.GetComponent<SocketConnection> ();
		_sock.setDelegate (this);
		_sock.fetchRules ();

		_selectedColor = ruleButton.colors;
		_unselectedColor = avatarButton.colors;
		_activeButton = ruleButton;

	}

	#region SocketConnectionInterface

	public void receiveData(string dt){

		// parse the response from server

		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];
		JObject resData = JObject.Parse (response ["data"].ToString ());


		// Get the rules list and give it to rulesContainer to render;

		JArray rules = JArray.Parse ((resData.GetValue ("rules")).ToString ());
		rulesContainer.GetComponent<RulesContainer> ().addRules (rules);

	}

	#endregion


	#region OnClick Handlers

	// Onclick handler for the start game button

	public void startGame(){
		
		int b = avatarContainer.GetComponent<avatars> ().selectedButton;


		SceneManager.LoadScene ("game");
	
	}

	// Hide and show containers

	private void hideandshow(int btIndex){

	}

	// Onclick handler for ruleButton, avatarButton

	public void changeActive(GameObject g){

		Button bt = g.GetComponent<Button> ();

		if (_activeButton.name != bt.name) {

			hideandshow (g.transform.GetSiblingIndex ());

			bt.colors = _selectedColor;
			_activeButton.colors = _unselectedColor;

			_activeButton = bt;

		}

	}

	#endregion
}
