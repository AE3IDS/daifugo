using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class RulesContainer : MonoBehaviour {

	public GameObject rulePrefab;

	public void addRules(JArray rules){

		// Render all the rules to the list

		for (int i = 0; i < rules.Count; i++) {

			JObject tk = JObject.Parse (rules.GetItem (i).ToString ());

			GameObject ruleItem = Instantiate (rulePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			ruleItem.GetComponent<RuleItem> ().addDetails (tk.GetValue ("name").ToString (), tk.GetValue ("description").ToString (), tk.GetValue ("ruleId").ToString ());
	
		}

	}



}
