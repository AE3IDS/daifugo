using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MasterLobbyContent : MonoBehaviour {

	public GameObject ruleContainer, avatarContainer;
	public Button ruleButton, avatarButton;

	private Button _activeButton;
	private RulesContainer _rules;

	private ColorBlock _selectedColor, _unselectedColor;

	// Use this for initialization
	void Start () {
	
		_rules = ruleContainer.GetComponent<RulesContainer> ();
		_selectedColor = ruleButton.colors;
		_unselectedColor = avatarButton.colors;
		_activeButton = ruleButton;

	}

	private void hideandshow(int btIndex){

		bool s = btIndex == 0 ? false : true;
		ruleContainer.SetActive (!s);
		avatarContainer.SetActive (s);

	}

	public int getAvatarIndex(){
		
		return avatarContainer.GetComponent<avatars> ().selectedButton;

	}

	public string[] getRules(){
		
		return _rules.getRules ();

	}
	
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
