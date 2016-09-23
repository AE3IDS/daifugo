using UnityEngine;
using System.Collections;

public class DistributeDeckScript : MonoBehaviour {


	GameObject clonedCard;

	void Awake(){
		clonedCard = Instantiate (gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
