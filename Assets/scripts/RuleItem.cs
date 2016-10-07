using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class RuleItem : MonoBehaviour {

	public GameObject ruleName;
	public GameObject ruleDescription;
	public Toggle toggle;

	private string _ruleId;

	void Start(){
		GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 1.0f, 1.0f);

	}

	public void addDetails(string name,string description,string id){
	
		ruleName.GetComponent<Text> ().text = name;
		ruleDescription.GetComponent<Text> ().text = description;
		_ruleId = id;

	}

	public void setToggleHandler(UnityAction<bool> d){
		toggle.onValueChanged.AddListener (d);

	}

	public bool isToggleOn(){

		return toggle.isOn;

	}

	public string getId(){
		return _ruleId;
	}

}
