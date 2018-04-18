using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace UnityEngine.XR.iOS{

	public class WallManager : MonoBehaviour {

		public GameObject firstWall;
		public GameObject secondWall;
		public GameObject thirdWall;
		public GameObject fourthWall;

		public Camera cam;

		private List<ARPlaneAnchorGameObject> planeAnchors = new List<ARPlaneAnchorGameObject>();
		private UnityARAnchorManager anchorManager;

		private GameObject activeWall;
		private List <GameObject> wallsList = new List<GameObject>();

		private Vector3 generatedPlaneSize;
		private Vector3 temp;

		private int actualActiveIteration = 0;
		private float amountToChange = 0.4f;

		public void Start(){
			CreateList ();
			anchorManager = GetComponent<UnityARAnchorManager> ();
		}

		public void Update(){
			GetActiveAnchors ();
			if (planeAnchors.Count != 0) {
				generatedPlaneSize = planeAnchors [0].planeAnchor.extent;
				Debug.Log ("AAAAAA" + planeAnchors.Count);
			}
			//		if (Input.GetTouch(0)) {
			//			Ray ray = cam.main.ScreenPointToRay(Input.GetTouch(0).position);
			//				RaycastHit hit;
			//				if (Physics.Raycast(ray, out hit)) {
			//				Debug.Log ("AAAAAA" + hit);
			//			}
			//		}
		} 

		private void GetActiveAnchors(){
			foreach (ARPlaneAnchorGameObject arpag in anchorManager.GetCurrentPlaneAnchors()) {
				planeAnchors.Add (arpag);
			}
		}

		private void CreateList(){
			activeWall = firstWall;
			wallsList.Add (firstWall);
			wallsList.Add (secondWall);
			wallsList.Add (thirdWall);
			wallsList.Add (fourthWall);
		}

		public void FitToHitBox(){
			activeWall.transform.localScale = generatedPlaneSize;
		}

		public void IncreaseHorizontal(){
			temp = activeWall.transform.localScale;
			temp.x += amountToChange;
			activeWall.transform.localScale = temp;
		}

		public void DecreaseHorizontal(){
			temp = activeWall.transform.localScale;
			temp.x -= amountToChange;
			activeWall.transform.localScale = temp;
		}

		public void IncreaseVertical(){
			temp = activeWall.transform.localScale;
			temp.z += amountToChange;
			activeWall.transform.localScale = temp;
		}

		public void DecreaseVertical(){
			temp = activeWall.transform.localScale;
			temp.z -= amountToChange;
			activeWall.transform.localScale = temp;
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
			for (int i = 0; i < wallsList.Count; i++) {
				if (wallsList [i].GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample> ().enabled == true) {
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

