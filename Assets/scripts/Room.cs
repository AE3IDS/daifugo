using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Room : MonoBehaviour {

	public GameObject roomId;
	public GameObject roomSeq;
	public GameObject numOfPlayer;


	void setroomid(string id){
		this.roomId.GetComponent<Text> ().text = this.roomId.GetComponent<Text> ().text + " " + id;
	}

	void setroomseq(string seq){
		this.roomSeq.GetComponent<Text> ().text = this.roomSeq.GetComponent<Text> ().text + " " + seq;
	}

	void setnumofplayer(string num){
		this.numOfPlayer.GetComponent<Text> ().text = this.numOfPlayer.GetComponent<Text> ().text + " " + num;
	}


}
