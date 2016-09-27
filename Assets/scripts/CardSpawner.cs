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

		Dictionary<string,string> _heartSuit, _diamondSuit, _clubSuit, _spadesSuit = new Dictionary<string, string> ();

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

	public GameObject produceCard(string type, string value1){

		string imageName = null;
		GameObject card = new GameObject();


		Dictionary <string,string> g = getDictionaryForKey (Int32.Parse (type));

		if (g != null) {
			g.TryGetValue(value1, out imageName);
		}

		// Add Component to the card GameOBject

		card.AddComponent<RectTransform> ();
		Image img = card.AddComponent<Image>();

		Texture2D tex = Resources.Load ("images/cards/"+imageName, typeof(Texture2D)) as Texture2D;
		img.sprite = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height), new Vector2 (0.0f, 0.0f));

		return card;
	}

	private Dictionary<string,string> getDictionaryForKey(int key){
		
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


}
