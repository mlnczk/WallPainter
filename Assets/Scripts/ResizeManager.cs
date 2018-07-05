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
		public GameObject showedText;

		public Button cutButton;
		private bool isInCutMode = false;

		public Camera cam;

		private List<ARPlaneAnchorGameObject> planeAnchors = new List<ARPlaneAnchorGameObject>();
		private UnityARAnchorManager anchorManager = new UnityARAnchorManager();


		private GameObject activeWall;
		private List <GameObject> listOfWalls = new List<GameObject>();

		private Vector3 bluePlaneSize;
		private Vector3 bluePlanePosition;

		private Vector3 tempHolder;

		private int actualActiveIteration = 0;
		private int amountOfObjectsInList;
		private float amountToChange = 0.4f;

		public void Start(){
			CreateList ();
		}

		public void Update(){
//			GetActiveAnchors ();
//			if (planeAnchors.Count != 0) {
//				bluePlaneSize = planeAnchors [0].planeAnchor.extent;
//			}
		} 

		private void GetActiveAnchors(){
			foreach (ARPlaneAnchorGameObject arpag in anchorManager.GetCurrentPlaneAnchors()) {
				planeAnchors.Add (arpag);
			}
			amountOfObjectsInList = planeAnchors.Count;
			bluePlaneSize = planeAnchors [amountOfObjectsInList - 1].planeAnchor.extent;
		}

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
			activeWall.transform.position = bluePlanePosition;

			showedText.GetComponent<Animator> ().Play("TextAnimation");
			}

		public void SwitchColors(Button pressedButton){
			activeWall.GetComponent<MeshRenderer> ().material.color = pressedButton.colors.normalColor;
		}
		public void AcceptButtonPressed(){
			CheckActiveWall ();

			SetupNextWall ();
		}

		public void CutButtonPressed(){
			if (isInCutMode == false) {
				isInCutMode = true;
				cutButton.GetComponentInChildren<Text> ().text = "STOP CUT";
				activeWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = false;
				ActivateCutting ();
			} else {
				isInCutMode = false;
				cutButton.GetComponentInChildren<Text> ().text = "START CUT";
				activeWall.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled = true;
				DisableCutting ();
			
			}
		}

		private void ActivateCutting(){
			firstWall.GetComponent<CutManager> ().enabled = true;
			secondWall.GetComponent<CutManager> ().enabled = true;
			thirdWall.GetComponent<CutManager> ().enabled = true;
			fourthWall.GetComponent<CutManager> ().enabled = true;

		}

		private void DisableCutting(){
			firstWall.GetComponent<CutManager> ().enabled = false;
			secondWall.GetComponent<CutManager> ().enabled = false;
			thirdWall.GetComponent<CutManager> ().enabled = false;
			fourthWall.GetComponent<CutManager> ().enabled = false;
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

