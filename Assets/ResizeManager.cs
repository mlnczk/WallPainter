using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeManager : MonoBehaviour {

	public GameObject plane;
	private Vector3 temp;
	private float amountToChange = 0.5f;


	public void IncreaseHorizontal(){
		temp = plane.transform.localScale;
		temp.x += amountToChange;
		plane.transform.localScale = temp;
	}

	public void DecreaseHorizontal(){
		temp = plane.transform.localScale;
		temp.x -= amountToChange;
		plane.transform.localScale = temp;
	}

	public void IncreaseVertical(){
		temp = plane.transform.localScale;
		temp.z += amountToChange;
		plane.transform.localScale = temp;
	}

	public void DecreaseVertical(){
		temp = plane.transform.localScale;
		temp.z -= amountToChange;
		plane.transform.localScale = temp;
	}
}
