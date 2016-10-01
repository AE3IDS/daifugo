using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class RoomListManager : MonoBehaviour,SocketConnectionInterface {

	public GameObject spinner;
	public GameObject j;
	private SocketConnection _sock;


	// Use this for initialization
	void Start () {
		spinner.SetActive (true);

		_sock = new SocketConnection ();
		_sock.setDelegate (this);
//		_sock.getRoom ();

	}

	public void receiveData(string dt){
//		Debug.Log (dt);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
