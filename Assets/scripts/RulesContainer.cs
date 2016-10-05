using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class RulesContainer : MonoBehaviour {

	public GameObject rulePrefab;
	bool _hasData =false;
	JArray _rules;

	List<string> selectedRules = new List<string>();

	void Start(){

		StartCoroutine ("addRulesCoroutine");

	}

	IEnumerator addRulesCoroutine(){

		while (true) {

			while (!_hasData) {
				yield return null;
			}
				
			for (int i = 0; i < _rules.Count; i++) {

				JObject tk = JObject.Parse (_rules.GetItem (i).ToString ());


				// Create new rulePrefab Item, set the content & parent

				GameObject ruleItem = Instantiate (rulePrefab, Vector3.zero, Quaternion.identity) as GameObject;

				RuleItem ri = ruleItem.GetComponent<RuleItem> ();
				ri.addDetails (tk.GetValue ("name").ToString (), tk.GetValue ("description").ToString (), tk.GetValue ("ruleId").ToString ());
				ri.setToggleHandler (delegate {
					toggleChange(ruleItem);	
				});

				ruleItem.transform.SetParent (transform);

			}

			_rules = null;
			yield break;
		}

	}

	#region public methods

	public string[] getRules(){

		return selectedRules.ToArray ();
	}

	public void addRules(JArray rules){
		_rules = rules;
		_hasData = true;
	}

	#endregion


	#region toggle change handler

	public void toggleChange(GameObject b){

		RuleItem ri = b.GetComponent<RuleItem> ();

		if (ri.isToggleOn ()) {
			selectedRules.Add (ri.getId ());
		} else {
			selectedRules.Remove (ri.getId ());
		}

	}

	#endregion


		
}
