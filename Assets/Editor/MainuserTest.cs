using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.UI;

[TestFixture]
public class MainuserTest {


	[Test]
	public void addCardTest(){

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		user.card = card;

		user.addCards (3, 3);

	}


	[Test]
	public void addCard_IncreaseCardCount(){

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		user.card = card;

		int numOfCards = 5;

		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			user.addCards (randomSuit, randomKind);
			Assert.AreEqual (i + 1, user.getCards ().Count);	
		}

	}


	[Test]
	public void addCard_CardDataInitialised(){

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		user.card = card;

		int numOfCards = 5;

		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			user.addCards (randomSuit, randomKind);
			GameObject[] cards = user.getCards ().ToArray ();

			CardScript s = cards [i].GetComponent<CardScript> ();
			Assert.IsTrue (s.cardSuit == randomSuit && s.cardRank == randomKind);
		}

	}

	[Test]
	public void addCard_IncreaseChildCount(){

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		user.card = card;

		int numOfCards = 5;

		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			user.addCards (randomSuit, randomKind);

			Assert.AreEqual (i + 1, j.transform.childCount);
		}

	}

	[Test]
	public void addCard_cardIsNotInteractable(){

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		user.card = card;

		int numOfCards = 5;

		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			user.addCards (randomSuit, randomKind);
		}

		foreach (GameObject obj in user.getCards()) {
			Assert.IsFalse (obj.GetComponent<Button> ().interactable); 
		}

	}

	[Test]
	public void addCard_cardHasCorrectCoordinate(){

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();
		float CARD_SPACE = 100.0f;
		float CARD_Y = 22.0f;
		float cardXMain = 87.0f;

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		user.card = card;

		int numOfCards = 5;

		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			user.addCards (randomSuit, randomKind);
		}

		foreach (GameObject obj in user.getCards()) {

			Vector3 size = obj.GetComponent<RectTransform> ().anchoredPosition3D;

			float xCoor = size.x;
			float yCoor = size.y;

			Assert.AreEqual (CARD_Y, yCoor);
			Assert.AreEqual (cardXMain, xCoor);

			cardXMain += CARD_SPACE;
		}

	}
		
	[Test]
	public void endDealtTest()
	{
		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();
		user.action = Resources.Load ("prefabs/action", typeof(GameObject)) as GameObject;

		user.endDealt ();
	}

	[Test]
	public void endDealtTest_DisableAction(){

		/* initialise sample object */

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();
		user.action = Resources.Load ("prefabs/action", typeof(GameObject)) as GameObject;


		user.toggleTurn ();
		user.endDealt ();
		Assert.AreEqual (false,user.action.activeSelf);

	}

	[Test]
	public void endDealtTest_SelectedCardsEmpty(){

		/* initialise sample object */

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();
		user.action = Resources.Load ("prefabs/action", typeof(GameObject)) as GameObject;


		/* add sample cards to the selected List */

		int numOfCards = 5;
		for (int i = 0; i < numOfCards; i++) {
			
			GameObject testCard = new GameObject();
			user.addSelected(j);

		}
			
		user.endDealt ();
		Assert.AreEqual (0,user.getSelected().Count);

	}

	[Test]
	public void endDealtTest_RemoveCardsFromParentHierarchy(){

		/* initialise sample object */

		GameObject j = new GameObject ();
		Mainuser user = j.AddComponent<Mainuser> ();
		user.init ();

		user.card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		user.action = Resources.Load ("prefabs/action", typeof(GameObject)) as GameObject;

		List<GameObject> tempSelected = new List<GameObject> ();

		/* add sample cards to the selected List */

		int numOfCards = 5;
		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			user.addCards (randomSuit, randomKind);

			if (i > 2) {
				GameObject[] temp = user.getCards ().ToArray ();
				tempSelected.Add (temp[i]);
				user.addSelected (temp [i]);
			}

		}

		user.endDealt ();

		foreach(GameObject sampleSelected in tempSelected){

			CardScript sampleCS = sampleSelected.GetComponent<CardScript> ();

			for(int i =0;i < j.transform.childCount;i++){

				GameObject child = j.transform.GetChild (i).gameObject;
				CardScript cS = child.GetComponent<CardScript> ();

				bool sameRank = cS.cardRank == sampleCS.cardRank;
				bool sameSuit = cS.cardSuit == sampleCS.cardSuit;

				if (sameRank && sameSuit) {

					Assert.IsFalse (child.activeSelf);
				}


			}
				
		}



		Assert.AreEqual (0,user.getSelected().Count);

	}



}
