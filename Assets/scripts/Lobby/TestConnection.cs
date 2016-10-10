using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;

public class TestConnection {

	private string _ip;
	private int _port;

	private TestConnectionInterface _delegate;

	public void setDelegate(TestConnectionInterface i){
		this._delegate = i;
	}

	public void startTest(){

		bool hasNoInternet = Application.internetReachability == NetworkReachability.NotReachable;
		this._delegate.giveStatus (!hasNoInternet);

	}



}
