using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
	private Dictionary <string,object> userId;



	IEnumerator receiveDataCoroutine(){

		while (true) {

			while (!_hasReceivedData)
				yield return null; 
			
			int temp = _responseCode;

			switch (_responseCode) {

				case Constant.GREET_CODE:
					greetCodeHandler ();
					break;

				case Constant.NEWPLAYER_CODE:
					newPlayerHandler ();
					break;
				
				case Constant.GAMEROOM_OCCUPIED:
					gameRoomOccupiedCodeHandler ();
					break;

				case Constant.DEALCARD_CODE:
					dealCardHandler ();
					break;

				case Constant.CARD_CODE:
					cardCodeHandler ();
					break;

				case Constant.STARTGAME_CODE:
					startGameHandler ();	
					break;
			}

			_hasReceivedData = !(temp == _responseCode);
		}

	}



	void changeRound(string roundInt){
		string[] g = _roundTextComponent.text.Split (' ');
		_roundTextComponent.text = g [0] + " " + roundInt;
	}


	private void newPlayerHandler(){
		
		string userId = _responseData.GetValue ("userId").ToString ();
		int photoId = _responseData.GetValue ("photoId").ToObject<int> ();

		_tableComponent.addUser (userId, photoId);
	}

	private void dealCardHandler(){

		string userId = (string)_responseData.GetValue ("userId");
		JArray cardsToken = (JArray)_responseData.SelectToken ("cards");

		List<int[]> cards = new List<int[]> ();

		foreach (JToken s in cardsToken) {
			JObject cardObject = (JObject)s;
			int[] card = new int[2]{(int) cardObject.GetValue ("_suit"),
				(int)cardObject.GetValue ("_kind") };
			cards.Add (card);
		}

		_tableComponent.displayDealt (userId, cards.ToArray());
	}


	private void gameRoomOccupiedCodeHandler(){
		
		_overlayscriptComponent.toggle ();
		_overlayscriptComponent.toggleWaitingText ();
		_socket.sendRequestForCards (userId);
	}


	private void cardCodeHandler(){

		JArray cardsArray = (JArray)_responseData.SelectToken("cards");
		int[,] cards = new int[cardsArray.Count, 2];

		for (int i = 0; i < cardsArray.Count; i++) {
			JObject j = JObject.Parse (cardsArray.GetItem (i).ToString ());
			cards [i, 0] = j.GetValue ("_suit").ToObject<int> ();
			cards [i, 1] = j.GetValue ("_kind").ToObject<int> ();
		}

		_tableComponent.card1 (cards);
		_socket.sendReady (userId);	
	}


	private void startGameHandler(){

		JToken turnPhotoToken = _responseData.GetValue ("turnPhotoId");
		int turnPhotoId = turnPhotoToken.ToObject<int> ();

		JToken turnIdToken = _responseData.GetValue ("turnUserId");
		string turnId = turnIdToken.ToString ();

		_tableComponent.initializeTable (turnId,turnPhotoId);
	}


	private void greetCodeHandler(){

		changeRound (_responseData ["round"].ToString ());
		_overlayscriptComponent.toggleLoadingText ();
		_overlayscriptComponent.toggleWaitingText ();

	}

	public void handleError(){

	}


	#region SocketConnectionInterface

	public void receiveData(string dt){

		JArray objects = JArray.Parse (dt);

		JToken response = objects.First["response"];
		_responseCode = (response ["code"]).ToObject<int>();
		_responseData = JObject.Parse (response ["data"].ToString ());

		_hasReceivedData = true;

	}

	#endregion

	#region Unity stuff

	void Start () {

		/* Initialize private variables */

		_roundTextComponent = round.GetComponent<Text> ();
		_tableComponent = gameTable.GetComponent<Table> ();
		_overlayscriptComponent = Overlay.GetComponent<OverlayScript> ();

		userId = new Dictionary<string, object> {
			{ "userId",PlayerPrefs.GetString ("user") }
		};


		/* find socket from prev scene and send greet message */

		_socket = (GameObject.FindWithTag ("socket")).GetComponent<SocketConnection> ();
		_socket.setDelegate (this);

		_socket.sendGreetMessage (userId);


		/* set user Id */

		_tableComponent.addOwnerId (PlayerPrefs.GetString ("user"));


		/* show overlay and loading text */

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
