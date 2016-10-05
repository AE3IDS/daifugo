using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class MasterLobby : MonoBehaviour,SocketConnectionInterface{

	public Button ruleButton, avatarButton;
	protected ColorBlock _selectedColor, _unselectedColor;
	public GameObject avatarContainer, rulesContainer, socket,spinner;

	private RulesContainer _rules;
	private SocketConnection _sock;
	private Button _activeButton;
	private string _tempId;
	private bool _hasReceivedRules;

	IEnumerator receiveRules(){
		
		while (!_hasReceivedRules) {
			yield return null;
		}
	
		spinner.SetActive (false);

	}

	void Start () {
		
		_sock = socket.GetComponent<SocketConnection> ();
		_sock.setDelegate (this);
		_sock.fetchRules ();

		spinner.SetActive (true);

		_hasReceivedRules = false;
		_rules = rulesContainer.GetComponent<RulesContainer> ();
		_selectedColor = ruleButton.colors;
		_unselectedColor = avatarButton.colors;
		_activeButton = ruleButton;

		StartCoroutine ("receiveRules");

	}

	#region SocketConnectionInterface

	public void receiveData(string dt){

		// parse the response from server

		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];
		JObject resData = JObject.Parse (response ["data"].ToString ());



		switch (response["code"].ToObject<int>()) {

			case Constant.SELECTEDRULE_CODE:
				
				break;

			case Constant.FETCHRULE_CODE:
			
				// Get the rules list and give it to rulesContainer to render;
				_hasReceivedRules = true;
				JArray rules = JArray.Parse ((resData.GetValue ("rules")).ToString ());
				_rules.addRules (rules);

				break;
		}
	}

	#endregion


	#region OnClick Handlers

	// Onclick handler for the start game button

	public void startGame(){


		// Send the rules and avatar selected

		int avatarIndex = avatarContainer.GetComponent<avatars> ().selectedButton;

		string m = "[" + string.Join (", ", _rules.getRules()) + "]" ;

		_sock.sendLobbyDetails (new Dictionary<string,object> { {"rules",m},{"avatar",avatarIndex}});
	}

	// Hide and show containers

	private void hideandshow(int btIndex){

		bool t = btIndex == 0?false:true;

		rulesContainer.SetActive (!t);
		avatarContainer.SetActive (t);

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
