using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Room : MonoBehaviour {

	public GameObject roomId;
	public GameObject roomSeq;
	public GameObject numOfPlayer;

	void Start(){
		GetComponent<RectTransform>().localScale = new Vector3(1.0f,1.0f,1.0f);
	}


	public void setRoomDetails(string seq, string id, string num){

		this.roomId.GetComponent<Text> ().text = this.roomId.GetComponent<Text> ().text + " " + id;
		this.roomSeq.GetComponent<Text> ().text = this.roomSeq.GetComponent<Text> ().text + seq;
		this.numOfPlayer.GetComponent<Text> ().text = this.numOfPlayer.GetComponent<Text> ().text + " " + num;
	}

}
