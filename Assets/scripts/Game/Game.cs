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
	private Dictionary <string,object> userId;


	void changeRound(string roundInt){
		string[] g = _roundTextComponent.text.Split (' ');
		_roundTextComponent.text = g [0] + " " + roundInt;
	}


	IEnumerator receiveDataCoroutine(){

		while (true) {

			while (!_hasReceivedData) {
				yield return null;
			}
				
			int temp = _responseCode;

			switch (_responseCode) {

				case Constant.GREET_CODE:
					changeRound (_responseData ["round"].ToString ());
					yield return new WaitForSeconds (0.6f);
					_overlayscriptComponent.toggleLoadingText ();
					_overlayscriptComponent.toggleWaitingText ();

					break;

				case Constant.NEWPLAYER_CODE:
					_tableComponent.addUser (_responseData.GetValue ("userId").ToString (), _responseData.GetValue ("photoId").ToObject<int> ());
					break;
				
				case Constant.GAMEROOM_OCCUPIED:
					_overlayscriptComponent.toggle ();
					_overlayscriptComponent.toggleWaitingText ();
					_socket.requestCard (userId);

					break;

				case Constant.CARD_CODE:
					JArray t = JArray.Parse (_responseData.GetValue ("cards").ToString ());
					int[,] cards = new int[t.Count, 2];
			
					for (int i = 0; i < t.Count; i++) {
						JObject j = JObject.Parse (t.GetItem (i).ToString ());
						cards [i, 0] = j.GetValue ("_suit").ToObject<int> ();
						cards [i, 1] = j.GetValue ("_kind").ToObject<int> ();
					}
			
					_tableComponent.card1 (cards);
					_socket.setReady (userId);	

					break;

				case Constant.STARTGAME_CODE:
					string turnId = _responseData.GetValue ("turn").ToString ();
					_tableComponent.distribute ();
					_tableComponent.switchTurn (turnId);
					
					break;
			}





			if (temp == _responseCode) {
				_hasReceivedData = false;
			}


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

		userId = new Dictionary<string, object> {
			{ "userId",PlayerPrefs.GetString ("user") }
		};

		_socket.greetServer (userId);


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
