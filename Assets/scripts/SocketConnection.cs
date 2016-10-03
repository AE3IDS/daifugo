using System.Collections;
using UnityEngine;
using System.Text;
using System.IO;
using System.Collections.Generic;
using WebSocketSharp;
using Newtonsoft.Json;

public class SocketConnection{


	/*
	 * 
	 * Code 101: greet server
	 * Code 102: new player
	 * Code 103: move
	 * Code 104: room list
	 * Code 105: fetch rule list
	 * 
	 * 
	 */

	private SocketConnectionInterface _delegator;
	private WebSocket _sock;
	List<string> requestPool = new List<string> ();

	public SocketConnection(){

		Debug.Log ("start connecting");

		_sock = new WebSocket ("ws://192.168.2.1:3000",new string[] { "echo-protocol" });

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

	}
		


	IEnumerator sendData(){

		while (true) {
			
			if ((_sock == null) ||  (!_sock.IsAlive || requestPool.Count == 0)) {
					yield return null;
			}

			foreach (string value in requestPool) {
				_sock.SendAsync (value, null);
			}

			requestPool.Clear ();
			 
		}
	}
		

	public void greetServer(){
		requestPool.Add (101);
	}


	public void getRoom(){
		requestPool.Add (104);
	}
		
	public void setDelegate(SocketConnectionInterface i){
		this._delegator = i;
	}




}
