using UnityEngine;
using System.Collections;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class MultiplayerLobby : MonoBehaviour,SocketConnectionInterface {

	public GameObject transporter;
	public GameObject roomContainer;
	public GameObject avatarContainer;

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


	public void joinButtonHandler()
	{

		// Show Loading

		// Send Join request to server

		RoomListContainer rlc = roomContainer.GetComponent<RoomListContainer> ();
		avatars avc = avatarContainer.GetComponent<avatars> ();					

		Dictionary<string,object> d = new Dictionary<string,object> () {
			{"roomId", rlc.getSelectedRoomToJoin()},
			{ "avatarId", avc.getAvatarSelection()}
		};

		Transporter t = transporter.GetComponent<Transporter> ();
		t.sendJoinRequest (d);

	}


	IEnumerator rcvDtCoroutine()
	{	
		while (true) 
		{
			while (_responseTk == null) 
				yield return null;


			switch (_responseCd) 
			{

				case Constant.LOBBYDETAILS_CODE:
				
					JObject responseObject = (JObject)_responseTk;
					string _tempId = (string)responseObject.GetValue("userId");
					PlayerPrefs.SetString ("user", _tempId);
					// Debug.Log(_tempId);
					//SceneManager.LoadScene ("game");

				break;

				case Constant.ROOMLIST_CODE:

					JArray rms = (JArray)_responseTk;
					RoomListContainer r = roomContainer.GetComponent<RoomListContainer> ();

					foreach (JToken s in rms) 
					{
						Dictionary<string,string> rmData = s.ToObject<Dictionary<string,string>> ();
						r.addRooms (rmData);
					}

				break;

			}
	
			_responseTk = null;
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
