using System.Collections;
using UnityEngine;
using System.Text;
using System.IO;
using System.Collections.Generic;
using WebSocketSharp;
using Newtonsoft.Json;

public class SocketConnection : MonoBehaviour{


	/*
	 * 
	 * Code 101: greet server
	 * Code 102: new player
	 * Code 103: move
	 * Code 104: room list
	 * Code 105: fetch rule list
	 * Code 106: selected rule list
	 * 
	 * 
	 */

	private SocketConnectionInterface _delegator;
	private WebSocket _sock;
	List<string> requestPool = new List<string> ();

	#region class definition

	IEnumerator sendData(){

		while (true) {

			while((_sock == null) ||  (!_sock.IsAlive || requestPool.Count == 0)) {
				yield return null;
			}
				
			foreach (string value in requestPool) {
				_sock.SendAsync (value, null);
			}

			requestPool.Clear ();

		}
	}

	void Start(){

		Debug.Log ("start connecting");

		_sock = new WebSocket ("ws://192.168.2.1:3000",null);

		//OnMessage event handler

		_sock.OnMessage += (sender, e) => {

			Debug.Log("receive message");

			if(e.IsText){
				Debug.Log(e.Data);
				(this._delegator).receiveData(e.Data);
			}

		};

		// OnError event handler

		_sock.OnError += (sender, e) => {
			Debug.Log("error occured");
			Debug.Log(e.Message);
		};

		_sock.ConnectAsync ();


		StartCoroutine ("sendData");

	}

	#endregion

	public void setDelegate(SocketConnectionInterface i){
		this._delegator = i;
	}


	public void sendLobbyDetails(Dictionary<string,object> dt){

		Debug.Log (JSONMaker.makeJSON (Constant.LOBBYDETAILS_CODE, dt));
		requestPool.Add (JSONMaker.makeJSON (Constant.LOBBYDETAILS_CODE,dt));
	}
		
	public void requestCard(Dictionary<string,object> dt){
		requestPool.Add (JSONMaker.makeJSON(Constant.REQUESTCARD_CODE,dt));
	}

	public void greetServer(Dictionary<string,object> dt){
		requestPool.Add (JSONMaker.makeJSON(Constant.GREET_CODE,dt));
	}

	public void fetchRules(){
		requestPool.Add (JSONMaker.makeJSON(Constant.FETCHRULE_CODE));
	}

	public void getRoom(){
		requestPool.Add (JSONMaker.makeJSON(Constant.ROOMLIST_CODE));
	}
		
}