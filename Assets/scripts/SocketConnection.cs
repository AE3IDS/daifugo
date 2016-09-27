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
	 * Code 104: 
	 * 
	 */

	private object _delegator;
	private WebSocket _sock;
//
	public void greet(){

		Debug.Log ("start connecting");
		string[] args = new string[] { "echo-protocol" };

		_sock = new WebSocket ("ws://dennyhartanto.com:3000",args);
		_sock.OnOpen += (sender,e) => {
			if(_sock.IsAlive){
				Debug.Log("connection is alive");
				((Table)this._delegator).distribute();
			}
			Debug.Log("connected");
		};
		_sock.OnError += (sender, e) => {
			Debug.Log("error occured");
			Debug.Log(e.Message);
		};
		_sock.ConnectAsync ();
	}
//
	public void setDelegate(object i){
		this._delegator = i;
	}




}
