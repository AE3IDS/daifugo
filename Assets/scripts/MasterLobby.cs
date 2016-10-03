using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class MasterLobby : MonoBehaviour,SocketConnectionInterface{

	public Button ruleButton;
	public Button avatarButton;

	public GameObject avatarContainer;
	public GameObject rulesContainer;

	protected Button _activeButton;
	protected SocketConnection _sock;
	protected ColorBlock _selectedColor, _unselectedColor;

	private string _tempId;

	void Start () {
			
		_sock = new SocketConnection ();
		_sock.fetchRules();


		_selectedColor = ruleButton.colors;
		_unselectedColor = avatarButton.colors;
		_activeButton = ruleButton;

	}

	public void receiveData(string dt){





	}

	// Onclick handler for the start game button

	public void startGame(){
		
		int b = avatarContainer.GetComponent<avatars> ().selectedButton;


		SceneManager.LoadScene ("game");
	
	}

	// Hide and show containers

	private void hideandshow(int btIndex){

	}

	// Onclick handler for ruleButton, avatarButton

	public void changeActive(GameObject g){

		Button bt = g.GetComponent<Button> ();

		if (_activeButton.name != bt.name) {

			hideandshow (g.transform.GetSiblingIndex ());

			bt.colors = _selectedColor;
			_activeButton.colors = _unselectedColor;

			_activeButton = bt;

		}

	}
}
