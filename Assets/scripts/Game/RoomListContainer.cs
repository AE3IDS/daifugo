using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomListContainer : MonoBehaviour {


	private GameObject selectedRoom = null;


	public void toggleHandler(GameObject s)
	{

		if(this.selectedRoom != null)
		{
			this.selectedRoom.GetComponent<RoomItem> ().disableCheck ();
		}

		this.selectedRoom = s;
	}


	public void addRooms(Dictionary<string,string> roomDetails)
	{

		GameObject room = Resources.Load ("prefabs/roomItem", typeof(GameObject)) as GameObject;
		GameObject newRoom = Instantiate (room, Vector3.zero, Quaternion.identity, transform) as GameObject;

		string roomName = null;
		string numOfPlayer = null;
		string rules = null;

		roomDetails.TryGetValue ("roomId", out roomName);
		roomDetails.TryGetValue ("numOfPlayers", out numOfPlayer);
		roomDetails.TryGetValue ("rules", out rules);

		newRoom.GetComponent<RoomItem> ().setItemDetails (rules, roomName, numOfPlayer);
		newRoom.GetComponent<RoomItem> ().setToggleUIHandler (delegate{
			this.toggleHandler (newRoom);
		});

	}

}
