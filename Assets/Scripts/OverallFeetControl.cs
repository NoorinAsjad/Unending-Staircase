using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class OverallFeetControl : MonoBehaviour {
	public AudioSource audio;
	public float hitLeft=0;
	public float hitRight = 0;
	Transform childL;
	Transform childR;
	public Vector3 bufferHeight;
	private RaycastHit RayL, RayR;
	//float speedFoot;
	float speedObject;
	public GameObject LC, RC;
	Vector3 leftV = new Vector3(0f,0f,0f);
	Vector3 rightV = new Vector3 (0f,0f,0f);
	float maxHeightclimbed = 0f;
	float minHeightclimbed = 145f;

	string filename;

	SteamVR_Controller.Device leftController {get
		{
			return SteamVR_Controller.Input((int)leftDevice.index);
		}
	}
	SteamVR_Controller.Device rightController {get
		{
			return SteamVR_Controller.Input((int)rightDevice.index);
		}
	}
	SteamVR_TrackedObject leftDevice, rightDevice;

	Vector3 dwdL=new Vector3(0, -1, 0);
	Vector3 dwdR=new Vector3(0, -1, 0);

	// Use this for initialization
	void Start () {
		audio.Play ();
		filename = "SubjectData\\"+ProjectManager.instance.fileName+"-"+ProjectManager.instance.experimentOrder[ProjectManager.instance.expNumber]+".txt";
		childL = LC.gameObject.transform.GetChild(1);
		childR = RC.gameObject.transform.GetChild(1);
		maxHeightclimbed = transform.position.y;
		minHeightclimbed = transform.position.y;

		using (StreamWriter sw = File.AppendText ("ResearchSubjects.txt")) {
			sw.Write ("Height at start: "+maxHeightclimbed+ " ");
		}
		
		//speedFoot = 0.1f;
		speedObject = 0.05f;
		bufferHeight = new Vector3(0, 1, 0);
		//leftFootPos = childL.transform.position;
		//rightFootPos = childR.transform.position;
		using (StreamWriter sw = File.AppendText (filename)) {
			sw.Write ("transform.position.x,");
			sw.Write ("transform.position.y,");
			sw.Write ("transform.position.z,");
			sw.Write ("transform.rotation.x,");
			sw.Write ("transform.rotation.y,");
			sw.Write ("transform.rotation.z,");

			sw.Write ("LC.transform.position.x,");
			sw.Write ("LC.transform.position.y,");
			sw.Write ("LC.transform.position.z,");
			sw.Write ("LC.transform.rotation.x,");
			sw.Write ("LC.transform.rotation.y,");
			sw.Write ("LC.transform.rotation.z,");

			sw.Write ("RC.transform.position.x,");
			sw.Write ("RC.transform.position.y,");
			sw.Write ("RC.transform.position.z,");
			sw.Write ("RC.transform.rotation.x,");
			sw.Write ("RC.transform.rotation.y,");
			sw.WriteLine ("RC.transform.rotation.z\n");
		}

	}


	// Update is called once per frame
	void FixedUpdate() {
		if (Input.GetKeyDown ("f")) {
			transform.position = new Vector3 (transform.position.x, 36.035f, transform.position.z);
		}

		//MAKE SURE THE CONTROLLERS HAVE IGNORE RAYCAST LAYER SELECTED

		//for the left device
		if (Physics.Raycast(LC.transform.position + bufferHeight, dwdL, out RayL, Mathf.Infinity))
		{
			Debug.DrawLine(LC.transform.position + bufferHeight, RayL.point, Color.red);
			if (RayL.collider.tag == "stairs" ) {
				hitLeft = RayL.point.y;
				childL.transform.position = new Vector3 (RayL.point.x - leftV.x, hitLeft - leftV.y, RayL.point.z - leftV.z);

			}
		}

		//for the right device
		if (Physics.Raycast(RC.transform.position + bufferHeight, dwdR, out RayR, Mathf.Infinity))
		{
			Debug.DrawLine(RC.transform.position + bufferHeight, RayR.point, Color.red);
			if (RayR.collider.tag == "stairs") {
				hitRight = RayR.point.y;
				childR.transform.position = new Vector3 (RayR.point.x - rightV.x, hitRight - rightV.y, RayR.point.z - rightV.z);
			}
		}

		using (StreamWriter sw = File.AppendText (filename)) { // records position and orientation of this.transform and the controllers
			sw.Write (transform.position.x + ",");
			sw.Write (transform.position.y + ",");
			sw.Write (transform.position.z + ",");
			sw.Write (transform.rotation.x + ",");
			sw.Write (transform.rotation.y + ",");
			sw.Write (transform.rotation.z + ",");

			sw.Write (LC.transform.position.x + ",");
			sw.Write (LC.transform.position.y + ",");
			sw.Write (LC.transform.position.z + ",");
			sw.Write (LC.transform.rotation.x + ",");
			sw.Write (LC.transform.rotation.y + ",");
			sw.Write (LC.transform.rotation.z + ",");

			sw.Write (RC.transform.position.x + ",");
			sw.Write (RC.transform.position.y + ",");
			sw.Write (RC.transform.position.z + ",");
			sw.Write (RC.transform.rotation.x + ",");
			sw.Write (RC.transform.rotation.y + ",");
			sw.WriteLine (RC.transform.rotation.z + "\n");
		}


		if (Mathf.Abs (hitLeft - hitRight) < 0.5f) { //Mathf.Abs (hitLeft - hitRight) < 0.5f

			if (hitLeft < hitRight - 0.01) { // when leftDevice is at a lower ground
				transform.position = new Vector3 (0, Mathf.Lerp (transform.position.y, hitLeft, speedObject), 0);

				//Mathf.Lerp(childR.transform.position.x, RayR.point.x, speed)

			} else if (hitLeft > hitRight + 0.01) { // when rightDevice is at a lower ground
				transform.position = new Vector3 (0, Mathf.Lerp (transform.position.y, hitRight, speedObject), 0);


			} else {
				transform.position = new Vector3 (0, Mathf.Lerp (transform.position.y, hitLeft, speedObject), 0);

			}
		}
		if (transform.position.y > maxHeightclimbed) {
			maxHeightclimbed = transform.position.y;
		}
		if (transform.position.y < minHeightclimbed) {
			minHeightclimbed = transform.position.y;
		}
	}

	public float maxHeightAccessor(){
		return maxHeightclimbed;
	}

	public float minHeightAccessor(){
		return minHeightclimbed;
	}
}
