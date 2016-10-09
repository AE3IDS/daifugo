using UnityEngine;
using System.Collections;

public class OverlayScript : MonoBehaviour {

	public GameObject pause;
	public GameObject info;
	public GameObject waitingText;
	public GameObject loadingText;
	public GameObject cards;

	private bool _showLoading = false;
	private bool _showWaiting = false;
	private bool _showCards = false;
	private bool _show = false;


	public void toggle(){
		gameObject.SetActive (!_show);
		_show = !_show;
	}

	public void toggleCards(){
		cards.SetActive (!_showCards);
		cards.GetComponent<Animator> ().SetBool ("show", true);
		_showCards = !_showCards;
	}

	public void toggleWaitingText(){
		waitingText.SetActive (!_showWaiting);
		_showWaiting = !_showWaiting;
	}

	public void toggleLoadingText(){
		loadingText.SetActive (!_showLoading);
		_showLoading = !_showLoading;
	}

}
