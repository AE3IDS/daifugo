using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour,SocketConnectionInterface {

	public Button ruleButton;
	public Button avatarButton;

	private Button activeButton = null;
	public GameObject listContainer;
	public GameObject spinner;
	private bool hasRuleData;
	private bool hasAvatarData;

	// ColorBlocks of the ruleButton and avatarButton
	private ColorBlock selectedColorBlock;
	private ColorBlock unselectedColorBlock;

	private const int RuleContainerIndex = 0;
	private const int AvatarContainerIndex = 1;

	void Start () {

		// init private variables

		hasRuleData = false;
		hasAvatarData = false;

		selectedColorBlock = ruleButton.colors;
		unselectedColorBlock = avatarButton.colors;

		// Register onclick event handler for the ruleButton & avatarButton

		ruleButton.onClick.AddListener (delegate {
			changeActive(ruleButton);
		});

		avatarButton.onClick.AddListener (delegate {
			changeActive (avatarButton);
		});

		changeActive (ruleButton);
	}





	// Hide and show containers

	private void hideandshow(int hide,int show){

		listContainer.transform.GetChild (hide).gameObject.SetActive (false);
		listContainer.transform.GetChild (show).gameObject.SetActive (true);

	}

	public void receiveAvatars(string[] s){

	}

	public void startGame(){
		SceneManager.LoadScene ("game");
	}

	// Onclick handler for ruleButton & avatarButton

	private void changeActive(Button bt){

		if (activeButton == null) {

			// Load data from server
//			spinner.SetActive(true);

			activeButton = bt;
		} else {
			
			if (activeButton.name != bt.name) {
				bt.colors = selectedColorBlock;

				if (bt.name == "ruleButton") {
					hideandshow (AvatarContainerIndex, RuleContainerIndex);
				} else {
					hideandshow (RuleContainerIndex, AvatarContainerIndex);
				}

				activeButton.colors = unselectedColorBlock;
				activeButton = bt;
			}

		}




	}
	

	void Update () {
	
	}

}
