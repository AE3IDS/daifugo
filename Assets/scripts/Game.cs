using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {


	// public variables

	public GameObject gameTable;
	public GameObject userAction;
	public GameObject Overlay;

	void Start () {

//		Overlay.SetActive (true);
//
//		Overlay.GetComponent<OverlayScript>().toggleLoadingText();
//
		Table t = gameTable.GetComponent<Table> ();
	}

	void Update () {


	}

	/*
	 * 
	 * OnClick handler for pause button
	 * 
	 */

	public void pauseButton(){
		Overlay.SetActive (true);
		Overlay.GetComponent<OverlayScript> ().showPause ();

	}

	/*
	 * 
	 * OnClick handler for info button
	 * 
	 */
	 
	public void infoButton(){
		Overlay.SetActive (true);
		Overlay.GetComponent<OverlayScript> ().showInfo ();
	}


}
