using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class TestConnection {

	private string _ip;
	private int _port;

	private TestConnectionInterface _delegate;

	public TestConnection(string url, int port){

		this._ip = url;
		this._port = port;

	}

	public void setDelegate(TestConnectionInterface i){
		this._delegate = i;
	}

	public void startTest(){

		bool success = true;
		TcpClient testClient = null;

		try{

			testClient = new TcpClient (this._ip,this._port);

		}

		catch(SocketException e){
			success = false;
			this._delegate.giveStatus (false);

		}

		finally{
			
			if (success) {

				testClient.Close ();
				this._delegate.giveStatus (true);

			}
		}

	} // end function

}
