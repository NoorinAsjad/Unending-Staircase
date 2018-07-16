using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class FeetCalibrater : MonoBehaviour {

	public GameObject LeftFootModel;
	public GameObject RightFootModel;
	public GameObject LeftFoot;
	public GameObject RightFoot;
	public GameObject RoomObj;
	OverallFeetControl feetControls;

	// Use this for initialization
	void Start () {
		LeftFoot.SetActive (true);
		RightFoot.SetActive (true);
		SteamVR_TrackedObject a = RightFoot.GetComponent<SteamVR_TrackedObject> ();
		a.index = SteamVR_TrackedObject.EIndex.Device4;
		//SteamVR_TrackedObject b = LeftFoot.GetComponent<SteamVR_TrackedObject> ();
		//b.index = SteamVR_TrackedObject.EIndex.Device5;
		feetControls = FindObjectOfType<OverallFeetControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("c")) {
			//Quaternion rotation = Quaternion.LookRotation (Vector3.Cross (new Vector3 (0, -1, 0), LeftFoot.transform.position - RightFoot.transform.position), new Vector3 (0, -1, 0));
			Quaternion rotation2 = Quaternion.LookRotation (LeftFoot.transform.position - RightFoot.transform.position, new Vector3 (0, -1, 0));
			LeftFootModel.transform.rotation = rotation2;
			RightFootModel.transform.rotation = rotation2;
		}
		if (Input.GetKeyDown ("v") && ProjectManager.instance.expNumber<3) {
			ProjectManager.instance.expNumber++;
			string nextSceneToLoad = ProjectManager.instance.experimentOrder [ProjectManager.instance.expNumber];
			using (StreamWriter sw = File.AppendText ("ResearchSubjects.txt")) {
				sw.Write ("Max height reached " + feetControls.maxHeightAccessor () + " ");
				sw.Write ("Min height reached " + feetControls.minHeightAccessor () + ",");
				sw.Write ("Exp " + nextSceneToLoad + "- ");
			}
			if (ProjectManager.instance.expNumber == 3) {
				ProjectManager.instance.fourExpDone = true;
			}
			bool tempShoes = false;
			if (nextSceneToLoad.Length == 2) {
				tempShoes = true;
			}
			if (nextSceneToLoad [0] == 'r') {
				if (tempShoes) {
					SceneManager.LoadScene ("rickIsSecondAuthor");
				} else {
					SceneManager.LoadScene ("rickIsSecondAuthor 1");
				}
			} else {
				if (tempShoes) {
					SceneManager.LoadScene ("vastFinal");
				} else {
					SceneManager.LoadScene ("vastFinal 1");
				}
			}
				
		}
			

		if (Input.GetKeyDown ("d")) { //d for done. no?
			using (StreamWriter sw = File.AppendText ("ResearchSubjects.txt")) {
				if (ProjectManager.instance.fourExpDone) {
					sw.Write ("Max height reached " + feetControls.maxHeightAccessor () + " ");
					sw.WriteLine ("Min height reached " + feetControls.minHeightAccessor () + "\n");
				} else {
					sw.WriteLine ("\n");
				}
			}
			ProjectManager.instance.fourExpDone = false;
		}

		if (Input.GetKeyDown ("k")) {
			//Vector3 roomCalibrateDiff = (LeftFoot.transform.position + RightFoot.transform.position) / 2 - RoomObj.transform.position;
			RoomObj.transform.position = (LeftFoot.transform.position + RightFoot.transform.position) / 2 + new Vector3(2.137f,0f,1.3561f);
		}
	}
}
