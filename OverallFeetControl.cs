using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallFeetControl : MonoBehaviour {
	public float hitLeft=0;
	public float hitRight=0;
	float hitLeftz=0, hitRightz=0;
	float RCspeed = 0, LCspeed = 0;
	public Vector3 bufferHeight;
	private RaycastHit RayL, RayR;
	float speed;
	public GameObject LC, RC;

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
	Vector3 lastLeftPosition = Vector3.zero;
	Vector3 lastRightPosition = Vector3.zero;


	// Use this for initialization
	void Start () {
		
		speed = 0.05f;
		bufferHeight = new Vector3(0, 1, 0);
	}


	// Update is called once per frame
	void FixedUpdate() {
		//MAKE SURE THE CONTROLLERS HAVE IGNORE RAYCAST LAYER SELECTED


		//for the left device
		if (Physics.Raycast(LC.transform.position + bufferHeight, dwdL, out RayL, Mathf.Infinity))
		{
			Debug.DrawLine(LC.transform.position + bufferHeight, RayL.point, Color.red);
			if (RayL.collider.tag == "stairs")
			{
				hitLeft = RayL.point.y;
				hitLeftz = RayL.point.z;

			}
		}

		//for the right device
		if (Physics.Raycast(RC.transform.position + bufferHeight, dwdR, out RayR, Mathf.Infinity))
		{
			Debug.DrawLine(RC.transform.position + bufferHeight, RayR.point, Color.red);
			if (RayR.collider.tag == "stairs")
			{
				hitRight = RayR.point.y;
				hitRightz = RayR.point.z;

			}
		}



		if (hitLeft < hitRight-0.01) // when leftDevice is at a lower ground
		{
			transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, hitLeft, speed), 0);
			/*RCspeed = ((RC.transform.position.z - lastRightPosition.z) / Time.deltaTime);

			Vector3 tempPos = RC.transform.position;
			if (RCspeed < 0.05f) {
				tempPos.z = hitRightz;
				RC.transform.position = tempPos;
			}
			lastRightPosition = RC.transform.position;
			//Debug.Log ("rcspeed"+RCspeed);
			//Debug.Log ("LRP"+ lastRightPosition);*/


		}
		else if (hitLeft > hitRight+0.01) // when rightDevice is at a lower ground
		{
			transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, hitRight, speed), 0);
			/*LCspeed = ((LC.transform.position.z - lastLeftPosition.z) / Time.deltaTime);

			Vector3 tempPos = LC.transform.position;
			if (LCspeed < 0.05f) {
				tempPos.z = hitLeftz;
				LC.transform.position = tempPos;
			}
			lastLeftPosition = LC.transform.position;
			//Debug.Log ("LCSPEED"+LCspeed);
			//Debug.Log ("LLP"+lastLeftPosition);*/
		}
		else
		{
			transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, hitLeft, speed), 0);
			/*RCspeed = ((RC.transform.position.z - lastRightPosition.z) / Time.deltaTime);

			Vector3 tempPos = RC.transform.position;
			if (RCspeed < 0.05f) {
				tempPos.z = hitRightz;
				RC.transform.position = tempPos;
			}
			lastRightPosition = RC.transform.position;*/
		}

	}
}
