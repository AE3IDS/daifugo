using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DealtCardsRenderer : MonoBehaviour {

	private const float cardSpace = 45.0f;
	private Vector2 minCardAnchor = new Vector2 (0.5f, 0.5f);
	private Vector2 maxCardAnchor = new Vector2(0.5f,0.5f);

	public GameObject card;

	IEnumerator addCoroutine(float startX, int[][] cards){

		for (int i = 0; i < cards.Length; i++) {

			GameObject j = (GameObject) Instantiate (card, new Vector3 (startX, 0, 0), Quaternion.identity);

			RectTransform cardRect = j.GetComponent<RectTransform> ();

			j.transform.SetParent (gameObject.transform,true);
			j.GetComponent<Button> ().interactable = false;


			/* set size of the card */

			float containerHeight = gameObject.GetComponent<RectTransform> ().sizeDelta.y;
			cardRect.sizeDelta = new Vector2 (74.0f, containerHeight);


			j.GetComponent<CardScript> ().setCardDetails (cards[i][0], cards[i][1]);
			cardRect.anchorMin = minCardAnchor;
			cardRect.anchorMax = maxCardAnchor;

			cardRect.anchoredPosition3D = new Vector3 (startX, 0, 0);
			startX += cardSpace;

			yield return new WaitForSeconds (0.75f);
		}
	}


	public void displayCards(int[][] cards){

		float startX = 0.0f;

		if (cards.Length % 2 != 0) {
			int numOfLeftCards = cards.Length / 2;
			startX = numOfLeftCards * cardSpace * -1;
		}
			
		StartCoroutine (addCoroutine (startX, cards));
	}
}
