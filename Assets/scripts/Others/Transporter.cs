using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Transporter : MonoBehaviour{


	private SocketConnection _socket;
	private static Transporter _instance;

	void Awake () {
	
		_socket = (GameObject.FindWithTag ("socket")).GetComponent<SocketConnection> ();

	}


	public void sendGreet(Dictionary<string, object> data){

		_socket.sendJSON (Constant.GREET_CODE, data);

	}

	public void sendRequestCards(Dictionary<string, object> data){

		_socket.sendJSON (Constant.CARD_CODE, data);

	}


	public void sendReady(Dictionary<string, object> data){

		_socket.sendJSON (Constant.READY_CODE, data);

	}


	public void sendEndDistribute(Dictionary<string, object> data){

		_socket.sendJSON (Constant.ENDDISTRIBUTE, data);

	}


	public void setSocketDelegate(SocketConnectionInterface i){

		_socket.setDelegate (i);

	}

	public void requestRooms()
	{
		_socket.sendJSON (Constant.ROOMLIST_CODE, new Dictionary<string,object> (){ });
	}







}
