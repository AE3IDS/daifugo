using UnityEngine;
using System.Collections;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class MultiplayerLobby : MonoBehaviour,SocketConnectionInterface {

	public GameObject transporter;


	private JToken _responseTk;
	private int _responseCd;

	// Use this for initialization
	void Start () {
	
		/* Get list of rooms */
		Transporter t = transporter.GetComponent<Transporter> ();
		t.setSocketDelegate (this);
		t.requestRooms ();

	}
	
	public void receiveData(string dt)
	{
		JArray objects = JArray.Parse (dt);

		JToken response = objects.First["response"];
		_responseTk = response.SelectToken ("data");
		_responseCd = (int)response.SelectToken("code");
	}


	public void handleError()
	{

	}


	// Update is called once per frame
	void Update () {
	
	}
}
