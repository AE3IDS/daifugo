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

		PlayerPrefs.SetInt ("mode", btIndex+1);
		SceneManager.LoadScene ("main");

	}

}
