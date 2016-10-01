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
	 * 
	 */

	private SocketConnectionInterface _delegator;
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


	public void greet(){

		startSocket (writeRequest (101));

	}


	public void getRoom(){
		
		startSocket (writeRequest (104));
	}

	public string writeRequest(int code){

		StringBuilder sb = new StringBuilder ();
		StringWriter sw = new StringWriter (sb);

		JsonWriter writer = new JsonTextWriter (sw);

		writer.Formatting = Formatting.Indented;
		writer.WriteStartObject ();
		writer.WritePropertyName ("request");
		writer.WriteStartObject ();
		writer.WritePropertyName ("code");
		writer.WriteValue (code.ToString ());
		writer.WriteEndObject ();
		writer.WriteEndObject ();



		return sb.ToString ();
	}


		
	public void setDelegate(SocketConnectionInterface i){
		this._delegator = i;
	}




}
