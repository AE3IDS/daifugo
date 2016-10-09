using UnityEngine;
using System.Collections;

public class LobbyOverlay : MonoBehaviour {

	public GameObject loadingText, errorWindow;

	public void showLoadingText(){

		loadingText.SetActive (true);
	}

	public void showErrorWindow(){
		errorWindow.SetActive (true);
		errorWindow.GetComponent<Animator> ().SetBool ("showPause",true);
	
	}
}
