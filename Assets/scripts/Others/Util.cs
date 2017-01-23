using UnityEngine;
using System.Collections;

public class Util{

	public static Sprite getAvatarForId(int photoId){

		string imageName = null;

		if (photoId == 0) {

			imageName = "dino";

		} else if (photoId == 1) {

			imageName = "minion";

		} else if (photoId == 2) {

			imageName = "dory";

		} else if (photoId == 3) {

			imageName = "monster";

		} else if (photoId == 4) {

			imageName = "anger";

		} else if (photoId == 5) {

			imageName = "baymax";

		} else if (photoId == 6) {

			imageName = "dory";

		}

		Texture2D avaTex = Resources.Load ("images/avatar/"+imageName, typeof(Texture2D)) as Texture2D;

		return Sprite.Create (avaTex, new Rect (0, 0, avaTex.width, avaTex.height), new Vector2 (0.0f, 0.0f));

	}

}
