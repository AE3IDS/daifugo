using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardSpawner{

	private Dictionary<string,string> _clubSuit;
	private Dictionary<string,string> _heartSuit;
	private Dictionary<string,string> _diamondSuit;
	private Dictionary<string,string> _spadesSuit;


	public CardSpawner(){

		_heartSuit = new Dictionary<string, string> ();
		_clubSuit = new Dictionary<string, string> ();
		_diamondSuit = new Dictionary<string, string> ();
		_spadesSuit = new Dictionary<string, string> ();

		string[] type = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
		string[] r = new string[] { "_of_hearts", "_of_clubs", "_of_spades", "_of_diamonds" };

		for (int i = 0; i < r.Length; i++) {	
			for (int j = 0; j < type.Length; j++) {
				
				Dictionary<string,string> g = getDictionaryForKey (i);

				if (g != null) {
					g.Add (type [j],type[j].ToLower()+r[i] );
				}
			}
		}

	}

	public GameObject produceCard(string type, string value1, float width,float height){

		string imageName = null;
		GameObject card = new GameObject();

		Dictionary <string,string> g = getDictionaryForKey (Int32.Parse (type));

		if (g != null) {
			g.TryGetValue(value1, out imageName);
		}

		// Add Component to the card GameOBject

		RectTransform r = card.AddComponent<RectTransform> ();

		r.sizeDelta = new Vector2 (width, height);

		Image img = card.AddComponent<Image>();

		Texture2D tex = Resources.Load ("images/cards/"+ getFolderNameForKey(Int32.Parse(type)) + "/" + imageName, typeof(Texture2D)) as Texture2D;
		img.sprite = Sprite.Create (tex, new Rect (0, 0,tex.width, tex.height), new Vector2 (0.0f, 0.0f));

		return card;
	}

	private Dictionary<string,string> getDictionaryForKey(int key){

		Debug.Log (key.ToString ());

		if (key == 0) {
			return _heartSuit;
		} else if (key == 1) {
			return _clubSuit;
		} else if (key == 2) {
			return _diamondSuit;
		} else if (key == 3) {
			return _spadesSuit;
		}

		return null;

	}

	private string getFolderNameForKey(int key){

		if (key == 0) {
			return "hearts";
		} else if (key == 1) {
			return "club";
		} else if (key == 2) {
			return "diamonds";
		} else if (key == 3) {
			return "spades";
		}

		return null;

	}


}
