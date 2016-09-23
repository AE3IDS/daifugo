using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitingScript : MonoBehaviour {

	private float _time;
	private string _waiting;
	private int _numOfDots;
	private bool _isWaiting;

	void Awake(){

		_isWaiting = true;
		_numOfDots = 0;
		_waiting = gameObject.GetComponent<Text> ().text;
		_time = 0.0f;

	}
		
	// Update is called once per frame
	void Update () {

		if (_isWaiting) {

			_time = _time + Time.deltaTime;
			Text t = gameObject.GetComponent<Text> ();

			if (_numOfDots == 3) {
				if (_time > 0.5) {
					t.text = _waiting;
					_numOfDots = 0;
				}
			} else {
				if (_time > 1) {
					_numOfDots++;
					t.text = t.text + ". ";
					_time = 0;
				}
			}
		}
	}


}
