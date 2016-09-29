using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void singlePlayer(int btIndex){

		string type = btIndex == 0 ? "single" : "multi";
		PlayerPrefs.SetString ("mode", type);
		SceneManager.LoadScene ("main");

	}

}
