using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class RulesContainer : MonoBehaviour {

	public GameObject rulePrefab;
	bool _hasData =false;
	JArray _rules;

	void Start(){

		StartCoroutine ("addRulesCoroutine");

	}

	IEnumerator addRulesCoroutine(){

		while (true) {

			while (!_hasData) {
				yield return null;
			}

			// Render all the rules to the list
			for (int i = 0; i < _rules.Count; i++) {

				JObject tk = JObject.Parse (_rules.GetItem (i).ToString ());

				GameObject ruleItem = Instantiate (rulePrefab, Vector3.zero, Quaternion.identity) as GameObject;
				ruleItem.GetComponent<RuleItem> ().addDetails (tk.GetValue ("name").ToString (), tk.GetValue ("description").ToString (), tk.GetValue ("ruleId").ToString ());

			}

			_rules = null;
			yield break;
		}

	}

	public void addRules(JArray rules){
		_rules = rules;
		_hasData = true;
	}
		
}
