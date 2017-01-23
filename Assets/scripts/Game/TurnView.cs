using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnView : MonoBehaviour {

	public GameObject ava;

	public void setAvaTurnImage(int photoId){

		ava.GetComponent<Image> ().sprite = Util.getAvatarForId (photoId);

	}


}
