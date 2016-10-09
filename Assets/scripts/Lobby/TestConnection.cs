using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;

public class TestConnection {

	private string _ip;
	private int _port;

	private TestConnectionInterface _delegate;
	private TcpClient _s;

	public TestConnection(string url, int port){

		this._ip = url;
		this._port = port;
		_s = new TcpClient ();
	}

	public void setDelegate(TestConnectionInterface i){
		this._delegate = i;
	}

	public void startTest(){
		_s.BeginConnect(this._ip,this._port,new AsyncCallback(AcceptCallback),_s);
	}

	public  void AcceptCallback(IAsyncResult ar) {

		try{
			_s.EndConnect(ar);
			this._delegate.giveStatus(true);
		}
		catch(SocketException e){
			Debug.Log ("done " + e.ErrorCode.ToString() );
			this._delegate.giveStatus (false);
		}

	}

}
