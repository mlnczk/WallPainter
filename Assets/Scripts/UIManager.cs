using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public void EnableObject(GameObject gameObject){
		if (gameObject.activeSelf) {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (true);
		}
	}
}
