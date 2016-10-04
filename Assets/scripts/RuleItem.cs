using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RuleItem : MonoBehaviour {

	public GameObject ruleName;
	public GameObject ruleDescription;


	public void addDetails(string name,string description){
	
		ruleName.GetComponent<Text> ().text = name;
		ruleDescription.GetComponent<Text> ().text = description;

	}

}
