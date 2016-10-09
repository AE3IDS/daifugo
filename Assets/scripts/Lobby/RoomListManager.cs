using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class RoomListManager : MonoBehaviour {

	public GameObject spinner;
	public GameObject j;
	private SocketConnection _sock;
	private bool _hasReceivedData;
	private JArray _response;


	IEnumerator receiveDataCoroutine(){

		while (true) {

			while (!_hasReceivedData) {
				yield return null;
			}

			spinner.SetActive (false);
			for (int i = 0; i < _response.Count; i++) {

				GameObject obj = Instantiate(j,Vector3.zero,Quaternion.identity) as GameObject;
				obj.GetComponent<Room> ().setRoomDetails ((i+1).ToString (), _response.GetItem (i) ["roomId"].ToString(), _response.GetItem (i) ["players"].ToString());

				obj.transform.SetParent(transform);
	
			}


			yield break;

		}

	}

	// Use this for initialization
	void Start () {

		StartCoroutine ("receiveDataCoroutine");

		spinner.SetActive (true);

//		_sock = new SocketConnection ();
//		_sock.setDelegate (this);
//		_sock.getRoom ();

	}

//	public void receiveData(string dt){
//
//
//		var objects = JArray.Parse (dt);
//
//		JToken response = objects.First["response"];
//
//		_hasReceivedData = true;
//		Debug.Log (_hasReceivedData.ToString ());
//		_response =  JArray.Parse(response ["data"].ToString());
//
//	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
