using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.UI;

[TestFixture]
public class CardMakerTest {

	[Test]
	public void CardMaker_Constructor_InitializeCard()
	{
		CardMaker cardM = new CardMaker ();
		Assert.NotNull (cardM.getCard ());
	}

	[Test]
	public void Test_setCardInteractable(){

		CardMaker cardM = new CardMaker ();
		cardM.setCardInteractable (false);

		GameObject card = cardM.getCard ();
		Assert.IsTrue(card.GetComponent<Button> ().interactable == false);

	}

	[Test]
	public void Test_set3DPosition(){

		Vector3 pos = new Vector3 (2.0f, 3.0f, 4.0f);

		CardMaker cardM = new CardMaker ();
		cardM.set3DPosition (pos);

		GameObject card = cardM.getCard ();
		Assert.IsTrue(card.GetComponent<RectTransform>().anchoredPosition3D.Equals(pos));

	}


	[Test]
	public void Test_setCardDetails(){

		int randomSuit = (int)Random.Range (1f, 4f);
		int randomKind = (int)Random.Range (3f, 15f);

		CardMaker cardM = new CardMaker ();
		cardM.setCardDetails (randomSuit, randomKind);

		GameObject card = cardM.getCard ();
		Assert.IsTrue(card.GetComponent<CardScript>().cardSuit == randomSuit);
		Assert.IsTrue (card.GetComponent<CardScript> ().cardRank == randomKind);

	}




	[Test]
	public void Test_setCardSize(){

		Vector2 size = new Vector2 (2.0f, 3.0f);

		CardMaker cardM = new CardMaker ();
		cardM.setCardSize (size);

		GameObject card = cardM.getCard ();
		Assert.IsTrue(card.GetComponent<RectTransform>().sizeDelta == size);
	
	}


	[Test]
	public void Test_setCardParent(){

		GameObject j = new GameObject ();

		CardMaker cardM = new CardMaker ();
		cardM.setCardParent (j);

		Assert.IsTrue(cardM.getCard ().transform.IsChildOf (j.transform));
		Assert.IsTrue (j.transform.childCount == 1);

	}


	[Test]
	public void Test_setAnchor(){

		CardMaker cardM = new CardMaker ();

		Vector2 min = new Vector2 (2.0f, 3.0f);
		Vector2 max = new Vector2 (4.0f, 5.0f);

		cardM.setAnchor (min, max);

		GameObject j = cardM.getCard ();

		Assert.IsTrue (j.GetComponent<RectTransform> ().anchorMin.Equals (min));
		Assert.IsTrue (j.GetComponent<RectTransform> ().anchorMax.Equals (max));

	}


}
