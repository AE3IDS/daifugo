using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

	public Button ruleButton;
	public Button roomlistButton;
	public Button avatarButton;

	public GameObject avatarContainer;
	public GameObject listContainer;


	private Button activeButton;



	// ColorBlocks of the ruleButton and avatarButton

	private ColorBlock selectedColorBlock;
	private ColorBlock unselectedColorBlock;

	void Start () {


		string mode = PlayerPrefs.GetString ("mode");

		if (mode == "single") {
			
			roomlistButton.interactable = false;

		} else if (mode == "multi") {
			
			ruleButton.interactable = false;

		}

		selectedColorBlock = ruleButton.colors;
		unselectedColorBlock = avatarButton.colors;

		activeButton = ruleButton;

	}





	// Hide and show containers

	private void hideandshow(int btIndex){

		int childCount = listContainer.transform.childCount;

		for (int i = 0; i < childCount; i++) {
			listContainer.transform.GetChild (i).gameObject.SetActive (i != btIndex?false:true);
		}


	}

	public void startGame(){
		
		int b = avatarContainer.GetComponent<avatars> ().selectedButton;

		PlayerPrefs.SetInt("avatar",b);


		SceneManager.LoadScene ("game");

	
	}

	// Onclick handler for ruleButton & avatarButton

	public void changeActive(GameObject g){

		Button bt = g.GetComponent<Button> ();

		if (activeButton.name != bt.name) {

			int btIndex = g.transform.GetSiblingIndex ();

			hideandshow (btIndex);

			bt.colors = selectedColorBlock;
			activeButton.colors = unselectedColorBlock;
			activeButton = bt;

		}

	}
}
