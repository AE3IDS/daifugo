using UnityEngine;
using System.Collections;

public class MultiplayerLobby : MonoBehaviour,SocketConnectionInterface {

	// Use this for initialization
	void Start () {
	
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
