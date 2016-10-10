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

	public void hideErrorWindow(){

		if (gameObject.activeInHierarchy) {
			
			errorWindow.GetComponent<Animator> ().SetBool ("showPause", false);
			errorWindow.GetComponent<Animator> ().SetBool ("hidePause", true);
			Invoke ("deactivateWindow", 1.5f);

		}
	}

	void deactivateWindow(){
		errorWindow.SetActive (false);
		gameObject.SetActive (false);
	}

}
