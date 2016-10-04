using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RuleItem : MonoBehaviour {

	public GameObject ruleName;
	public GameObject ruleDescription;

	private string _ruleId;

	public void addDetails(string name,string description,string id){
	
		ruleName.GetComponent<Text> ().text = name;
		ruleDescription.GetComponent<Text> ().text = description;
		_ruleId = id;

	}

}
