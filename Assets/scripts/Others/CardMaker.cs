using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardMaker{

	GameObject card;

	public CardMaker(){

		GameObject card = (GameObject) Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		this.card = MonoBehaviour.Instantiate (card, Vector3.zero, Quaternion.identity) as GameObject;
	}


	public void set3DPosition(Vector3 pos){

		this.card.GetComponent<RectTransform> ().anchoredPosition3D = pos;

	}

	public void setCardSize(Vector2 size){

		this.card.GetComponent<RectTransform> ().sizeDelta = size;

	}


	public void setCardParent (GameObject j){

		this.card.transform.SetParent (j.transform,true);

	}


	public void addHandler(UnityEngine.Events.UnityAction handler){

		this.card.GetComponent<Button> ().onClick.AddListener (handler);

	}
		
	public void setCardDetails(int suit,int rank){

		this.card.GetComponent<CardScript> ().setCardDetails (suit, rank);

	}


	public void setCardInteractable(bool interactable){

		this.card.GetComponent<Button> ().interactable = interactable;

	}


	public GameObject getCard(){

		return this.card;
	}
}
