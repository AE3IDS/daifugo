using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using Newtonsoft.Json.Linq;

public class Game : MonoBehaviour {


	// public variables

	public GameObject gameTable;
	public GameObject Overlay;
	public GameObject round;

	private Table _table;
	private Text _roundTextComponent;
	private OverlayScript _overlayscriptComponent;
	private bool _hasReceivedData;
	private string _responseType;
	private JToken _responseData;




	private SocketConnection _socket;

	void Awake(){






		StartCoroutine ("receiveDataCoroutine");
	}

	void changeRound(string roundInt){
		string[] g = _roundTextComponent.text.Split (' ');
		_roundTextComponent.text = g [0] + " " + roundInt;
	}


	IEnumerator receiveDataCoroutine(){

		while (true) {

			while (!_hasReceivedData) {
				yield return null;
			}


			if (_responseType == "greet") {

				changeRound (_responseData ["round"].ToString ());
				_table.addOwnerId (_responseData ["user"].ToString ());

				yield return new WaitForSeconds (0.6f);
				_overlayscriptComponent.toggleLoadingText ();

//				

				_overlayscriptComponent.toggleWaitingText ();

			} else if (_responseType == "move") {

		
			} else if (_responseType == "user") {


			} else if (_responseType == "round") {

				changeRound (_responseData ["round"].ToString ());

			}

			_hasReceivedData = false;
		}

	}

	public void receiveData(string dt){

		var objects = JArray.Parse (dt);

		JToken response = objects.First["response"];
		_responseType = (response ["type"]).ToString ();
		_hasReceivedData = true;
		_responseData = response ["data"];

	}

	void Start () {

		// Initialize private variables

		_hasReceivedData = false;
		_roundTextComponent = round.GetComponent<Text> ();
		_table = gameTable.GetComponent<Table> ();
		_overlayscriptComponent = Overlay.GetComponent<OverlayScript> ();


		// Initialise websocket

		_socket = new SocketConnection ();
		_socket.setDelegate (this);
		_socket.greet ();


		// show loading text

		_overlayscriptComponent.toggle();
		_overlayscriptComponent.toggleLoadingText ();
	}

	void Update () {


	}
}
