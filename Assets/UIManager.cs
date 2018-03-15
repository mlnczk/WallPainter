using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public GameObject sizePanel;
	public GameObject colorPanel;

	public void EnableObject(GameObject gameObject){
		if (gameObject.activeSelf) {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (true);
		}
	}

	public void OpenColorPanel(){
		if (sizePanel.activeSelf) {
			sizePanel.SetActive (false);
		}
		if (colorPanel.activeSelf) {
			colorPanel.SetActive (false);
		} else {
			colorPanel.SetActive (true);
		}
	}


	public void OpenSizePanel(){
		if (colorPanel.activeSelf) {
			colorPanel.SetActive (false);
		}
		if (sizePanel.activeSelf) {
			sizePanel.SetActive (false);
		} else {
			sizePanel.SetActive (true);
		}
	}
}
