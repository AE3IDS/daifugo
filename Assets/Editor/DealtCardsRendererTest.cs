using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class DealtCardsRendererTest {

	[Test]
	public void NewCards_Added_IncreaseChild()
	{
		List<int[]> cards = new List<int[]> ();

		GameObject renderer = new GameObject ();
		DealtCardsRenderer r = renderer.AddComponent<DealtCardsRenderer> ();
		int numOfCards = 5;

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		r.card = card;

		/* prepare test case */

		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			int[] cardItem = new int[2]{ randomSuit, randomKind };

			cards.Add (cardItem);
		}

		r.displayCards (cards.ToArray ());
		int end = renderer.GetComponent<Transform> ().childCount;

		Assert.AreEqual(end,numOfCards);
	}

	[Test]
	public void OddNewCards_AddedHave_CorrectCoordinate(){

		List<int[]> cards = new List<int[]> ();

		/* Create object */

		GameObject renderer = new GameObject ();
		DealtCardsRenderer r = renderer.AddComponent<DealtCardsRenderer> ();


		/* Initialize properties of r */

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		r.card = card;

		int numOfCards = 5;
		float startX = (numOfCards / 2) * 45 * -1;
		float space = 45.0f;

	
		/* prepare test case */

		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			int[] cardItem = new int[2]{ randomSuit, randomKind };

			cards.Add (cardItem);
		}

		r.displayCards (cards.ToArray ());


		/* check x-coordinate */

		for (int i = 0; i < numOfCards; i++) {

			GameObject child = renderer.transform.GetChild (i).gameObject;
			float xCoor = child.GetComponent<RectTransform> ().anchoredPosition3D.x;

			Assert.AreEqual(startX,xCoor);
			startX += space;
		}

	}

	[Test]
	public void EvenNewCards_AddedHave_CorrectCoordinate(){

		List<int[]> cards = new List<int[]> ();

		/* Create object */

		GameObject renderer = new GameObject ();
		DealtCardsRenderer r = renderer.AddComponent<DealtCardsRenderer> ();


		/* Initialize properties of r */

		GameObject card = Resources.Load ("prefabs/maincard", typeof(GameObject)) as GameObject;
		r.card = card;

		int numOfCards = 4;
		float startX = (numOfCards / 2) * 45 * -1;
		float space = 45.0f;


		/* prepare test case */

		for (int i = 0; i < numOfCards; i++) {

			int randomSuit = (int)Random.Range (1f, 4f);
			int randomKind = (int)Random.Range (3f, 15f);
			int[] cardItem = new int[2]{ randomSuit, randomKind };

			cards.Add (cardItem);
		}

		r.displayCards (cards.ToArray ());


		/* check x-coordinate */

		for (int i = 0; i < numOfCards; i++) {

			GameObject child = renderer.transform.GetChild (i).gameObject;
			float xCoor = child.GetComponent<RectTransform> ().anchoredPosition3D.x;

			Assert.AreEqual(startX,xCoor);
			startX += space;
		}

	}


}
