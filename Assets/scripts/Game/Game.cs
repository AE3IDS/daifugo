using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using Newtonsoft.Json.Linq;

public class Game : MonoBehaviour,SocketConnectionInterface {


	// public variables

	public GameObject gameTable, Overlay, round;


	private Table _tableComponent;
	private Text _roundTextComponent;
	private OverlayScript _overlayscriptComponent;
	private bool _hasReceivedData = false;
	private int _responseCode;
	private JObject _responseData;
	private SocketConnection _socket;



	void changeRound(string roundInt){
		string[] g = _roundTextComponent.text.Split (' ');
		_roundTextComponent.text = g [0] + " " + roundInt;
	}


	IEnumerator receiveDataCoroutine(){

		while (true) {

			while (!_hasReceivedData) {
				yield return null;
			}
				

			if (_responseCode == Constant.GREET_CODE) {

				changeRound (_responseData ["round"].ToString ());

				yield return new WaitForSeconds (0.6f);

				_overlayscriptComponent.toggleLoadingText ();
				_overlayscriptComponent.toggleWaitingText ();

			} else if (_responseCode == Constant.NEWPLAYER_CODE) {

//				Debug.Log ("new player");

				_tableComponent.addUser (_responseData.GetValue ("userId").ToString(), _responseData.GetValue ("photoId").ToObject<int>());

			} else if (_responseCode == Constant.GAMEROOM_OCCUPIED) {

				Debug.Log ("occupied");

				// hide overlay

				_overlayscriptComponent.toggle();
				_overlayscriptComponent.toggleWaitingText ();


				//distribute cards

			}


			_hasReceivedData = false;
		}

	}

	#region SocketConnectionInterface

	public void receiveData(string dt){

		JArray objects = JArray.Parse (dt);

		JToken response = objects.First["response"];
		_responseCode = (response ["code"]).ToObject<int>();
		_responseData = JObject.Parse (response ["data"].ToString ());

		_hasReceivedData = true;

	}

	public void handleError(){
		Debug.Log ("game e");
	}

	#endregion

	#region Unity stuff

	void Start () {

		// Initialize private variables

		_roundTextComponent = round.GetComponent<Text> ();
		_tableComponent = gameTable.GetComponent<Table> ();
		_overlayscriptComponent = Overlay.GetComponent<OverlayScript> ();


		// find socket from prev scene and send greet message

		_socket = (GameObject.FindWithTag ("socket")).GetComponent<SocketConnection> ();
		_socket.setDelegate (this);

		Dictionary <string,object> obj = new Dictionary<string, object> {
			{ "userId",PlayerPrefs.GetString ("user") }
		};

		_socket.greetServer (obj);


		// show overlay and loading text

		_overlayscriptComponent.toggle();
		_overlayscriptComponent.toggleLoadingText ();
	}

	void Update () {


	}

	void Awake(){

		StartCoroutine ("receiveDataCoroutine");
	}

	#endregion
}
