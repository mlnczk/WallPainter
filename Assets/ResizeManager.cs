using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace UnityEngine.XR.iOS{

public class ResizeManager : MonoBehaviour {
	

		public GameObject firstWall;
		public GameObject secondWall;
		public GameObject thirdWall;
		public GameObject fourthWall;

		public Camera cam;

		private List<ARPlaneAnchorGameObject> planeAnchors = new List<ARPlaneAnchorGameObject>();
		private UnityARAnchorManager anchorManager = new UnityARAnchorManager();


		private GameObject activeWall;
		private List <GameObject> listOfWalls = new List<GameObject>();

		private Vector3 bluePlaneSize;
		private Vector3 bluePlanePosition;

		private Vector3 tempHolder;
		private GameObject tempGameObjectHolder;

		private int actualActiveIteration = 0;
		private int amountOfObjectsInList;
		private float amountToChange = 1f;

		public void Start(){
			CreateList ();
		}

		public void Update(){
			
		} 

//		private void DisableStaticWalls(){
//			foreach (GameObject gameObject in listOfWalls) {
//				gameObject.isStatic = false;
//			}
//		}

		private void CreateList(){
			activeWall = firstWall;
			listOfWalls.Add (firstWall);
			listOfWalls.Add (secondWall);
			listOfWalls.Add (thirdWall);
			listOfWalls.Add (fourthWall);
		}

		public void FitToHitBox(){
			GetActiveAnchors ();
			activeWall.transform.localScale = bluePlaneSize;
			}

		private void GetActiveAnchors(){
			foreach (ARPlaneAnchorGameObject arpag in anchorManager.GetCurrentPlaneAnchors()) {
				planeAnchors.Add (arpag);
			}
			amountOfObjectsInList = planeAnchors.Count;
			bluePlaneSize = planeAnchors [amountOfObjectsInList - 1].planeAnchor.extent;
		}

		public void IncreaseHorizontal(){
			tempHolder = activeWall.transform.localScale;
			tempHolder.x += amountToChange;
			activeWall.transform.localScale = tempHolder;
			}

		public void DecreaseHorizontal(){
			tempHolder = activeWall.transform.localScale;
			tempHolder.x -= amountToChange;
			activeWall.transform.localScale = tempHolder;
			}

		public void IncreaseVertical(){
			tempHolder = activeWall.transform.localScale;
			tempHolder.z += amountToChange;
			activeWall.transform.localScale = tempHolder;
			}

		public void DecreaseVertical(){
			tempHolder = activeWall.transform.localScale;
			tempHolder.z -= amountToChange;
			activeWall.transform.localScale = tempHolder;
			}

		public void SwitchColors(Button pressedButton){
			activeWall.GetComponent<MeshRenderer> ().material.color = pressedButton.colors.normalColor;
		}
		public void AcceptButtonPressed(){
			CheckActiveWall ();
			SetupNextWall ();
		}

		//checks which wall is actually active and reset to next one
		private void CheckActiveWall(){
			for (int i = 0; i < listOfWalls.Count; i++) {
				if (listOfWalls [i].GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled == true) {
					actualActiveIteration = i;
				} 
			}
		}

		//iterate between plane objects in list to enable us mutliwalling
		private void SetupNextWall(){
			switch (actualActiveIteration) {
			case 0:
				firstWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = false;
				secondWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = true;
				activeWall = secondWall;
				break;
			case 1:
				secondWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = false;
				thirdWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = true;
				activeWall = thirdWall;
				break;
			case 2:
				thirdWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = false;
				fourthWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = true;
				activeWall = fourthWall;
				break;
			case 3:
				fourthWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = false;
				firstWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = true;
				activeWall = firstWall;
				break;
			default:
				activeWall = firstWall;
				break;
			}
		}
	}
}

