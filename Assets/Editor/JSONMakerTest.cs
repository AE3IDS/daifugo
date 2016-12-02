using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class JSONMakerTest {

	[Test]
	public void testMakeJSONArray()
	{
		int sampleCode = 110;
		int numOfSelectedCards = 3;

		List<Dictionary<string,int>[]> s = new List<Dictionary<string, int>[]> ();
		int randomSuit = -1;
		int randomKind = -1;

		string expectedOutput = "{\"code\":" + sampleCode + ",\"data\":[";

		for (int i = 0; i < numOfSelectedCards; i++) {
			
			Dictionary<string,int>[] cardItem = new Dictionary<string, int> [2];

			randomSuit = (int)Random.Range (1f, 4f);
			Dictionary<string,int> cardSuit = new Dictionary<string,int> ();
			cardSuit.Add ("suit",randomSuit);

			randomKind = (int)Random.Range (1f, 15f);
			Dictionary<string,int> cardRank = new Dictionary<string,int> ();
			cardRank.Add ("kind",randomKind);

			cardItem [0] = cardSuit;
			cardItem [1] = cardRank;

			s.Add (cardItem);
			string m = "{\"suit\":" + randomSuit + ",\"kind\":" + randomKind + "}";
			m += (i != 3 - 1) ? "," : "";
			expectedOutput += m;
		}

		expectedOutput += "]}";


		string outputJSON = JSONMaker.makeJSONArray (sampleCode, s.ToArray());
		outputJSON = outputJSON.Replace ("\n", "");
		outputJSON = outputJSON.Replace (" ", "");

		Assert.AreEqual(outputJSON, expectedOutput);
	}
}
