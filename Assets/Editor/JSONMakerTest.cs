using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class JSONMakerTest {

	[Test]
	public void testMakeJSONArray(){
		
		int numOfSelectedCards = 3;

		List<Dictionary<string,int>> s = new List<Dictionary<string, int>> ();
		int randomSuit = -1;
		int randomKind = -1;

		string expectedOutput = "{\"code\":" + Constant.DEALCARD_CODE + ",\"userId\":\"ASDFS\"" + ",\"data\":[";


		/* initialize heading of json */

		Dictionary<string,object> heading = new Dictionary<string, object> {
			{"code",Constant.DEALCARD_CODE},
			{"userId","ASDFS"}
		};


		/* initialize main data of json */

		for (int i = 0; i < numOfSelectedCards; i++) {

			randomSuit = (int)Random.Range (1f, 4f);
			randomKind = (int)Random.Range (1f, 15f);

			Dictionary<string,int> cardDetails = new Dictionary<string,int> {
				{ "suit",randomSuit },
				{ "kind",randomKind }
			};

			s.Add (cardDetails);
			string m = "{\"suit\":" + randomSuit + ",\"kind\":" + randomKind + "}";
			m += (i != 3 - 1) ? "," : "";
			expectedOutput += m;
		}

		expectedOutput += "]}";


		string outputJSON = JSONMaker.makeJSONArray (heading, s.ToArray());
		outputJSON = outputJSON.Replace ("\n", "");
		outputJSON = outputJSON.Replace (" ", "");

		Assert.AreEqual(outputJSON, expectedOutput);
	}
}
