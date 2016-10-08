using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class TestConnection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	



		try{

			TcpClient testClient = new TcpClient ("192.168.2.1",3000);

		}
		catch(SocketException e){

			Debug.Log ("error connecting");

		}



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
