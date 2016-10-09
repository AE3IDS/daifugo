using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class MasterLobby : MonoBehaviour,SocketConnectionInterface,TestConnectionInterface{

	public GameObject socket, spinner, overlay, Content;
	public string url;
	public int port;


	private SocketConnection _sock;
	private string _tempId;
	private bool _hasReceivedRules = false;
	private bool _hasReceiveId = false;

	private int success = -1;

	private MasterLobbyContent _ct;

	void Start () {

		DontDestroyOnLoad (socket);
	
	
		_ct = Content.GetComponent<MasterLobbyContent> ();

		TestConnection u = new TestConnection (url, port);
		u.setDelegate (this);
		u.startTest ();


		_sock = socket.GetComponent<SocketConnection> ();
		_sock.setDelegate (this);


		StartCoroutine ("receiveRules");
		StartCoroutine ("receiveConnectionStatus");

	}

	#region Socket Connection

	public void receiveData(string dt){

		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];
		JObject resData = JObject.Parse (response ["data"].ToString ());

		switch (response["code"].ToObject<int>()) {

			case Constant.LOBBYDETAILS_CODE:
			
				_tempId = resData.GetValue ("userId").ToString ();
				_hasReceiveId = true;

				break;

			case Constant.FETCHRULE_CODE:
			
				// Get the rules list and give it to rulesContainer to render;

				// _hasReceivedRules = true;

				// JArray rules = JArray.Parse ((resData.GetValue ("rules")).ToString ());
				// _rules.addRules (rules);

				break;
		}	
	}

	public void handleError(){
		Debug.Log ("lobby error");
	}

	IEnumerator receiveRules(){

		while (true) {

			while (/*!_hasReceivedRules ||*/ !_hasReceiveId ) {
				yield return null;
			}

			// When received Id from server

			if(_hasReceiveId){
				PlayerPrefs.SetString ("user", _tempId);
				SceneManager.LoadScene ("game");

			}

			break;
		}
	}

	#endregion


	#region Test Connection
		
	public void giveStatus(bool s){
		success = s ? 1 : 0;
	}

	IEnumerator receiveConnectionStatus(){

		while (true) {

			while (success == -1) {
				yield return null;
			}

			Debug.Log (success);

			if (success == 1) {

			} else if(success == 0){
				overlay.SetActive (true);
			}

			break;
		}
	}

	#endregion


	#region OnClick Handlers

	public void startGame(){

		overlay.SetActive (true);


		int avatarIndex = _ct.getAvatarIndex ();

		string m = "[" + string.Join (", ", _ct.getRules ()) + "]";

		Dictionary<string,object> obj = new Dictionary<string,object> { 
			{ "rules",m },
			{ "avatar",1 },
			{ "mode",1 }
		};

		_sock.sendLobbyDetails (obj);
	}

	#endregion
}
