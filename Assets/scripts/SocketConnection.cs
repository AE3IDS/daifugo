using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using WebSocketSharp;

public class SocketConnection{


	/*
	 * 
	 * Code 101: greet server
	 * Code 102: new player
	 * Code 103: move
	 * Code 104: room list
	 * 
	 */

	private object _delegator;
	private WebSocket _sock;

	private void startSocket(string data){

		Debug.Log ("start connecting");

		_sock = new WebSocket ("ws://dennyhartanto.com:3000",new string[] { "echo-protocol" });


		// OnOpen event handler 

		_sock.OnOpen += (sender,e) => {
			Debug.Log("connected");
			_sock.SendAsync(data,null);
		};


		//OnMessage event handler

		_sock.OnMessage += (sender, e) => {

			Debug.Log("receive message");

			if(e.IsText){
				Debug.Log(e.Data);
				((Game)this._delegator).receiveData(e.Data);
			}

		};

		// OnError event handler

		_sock.OnError += (sender, e) => {
			Debug.Log("error occured");
			Debug.Log(e.Message);
		};

		_sock.ConnectAsync ();

	}


	public void greet(){



	}


	public void getRoom(){


	}






//
	public void setDelegate(object i){
		this._delegator = i;
	}




}
