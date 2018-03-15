using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintManager : MonoBehaviour {

	public GameObject plane;
	public GameObject palette;

	public void SwitchColors(Button pressedButton){
		plane.GetComponent<MeshRenderer> ().material.color = pressedButton.colors.normalColor;
	}
}
