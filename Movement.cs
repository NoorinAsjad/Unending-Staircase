using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	GameObject ego;
	RaycastHit hit;
	Vector3 dwd;
	float groundy; // keep track of the y value of the ground beneath the player
	public float speed = 0.05f;
	//float initialHeight;
	Transform Parent;

	void Start () {
		//ego = GameObject.Find ("Main Camera");
		groundy = 0;
		dwd = new Vector3 (0, -1, 0);
		Parent = transform.parent;
		Debug.Log ("blah started");
	}


	// Update is called once per frame
	void FixedUpdate () {
		
		if (Physics.Raycast (transform.position, dwd, out hit, Mathf.Infinity)) {
			Debug.DrawLine (transform.position, hit.point, Color.red);
			if (hit.collider.tag == "ground") {
				Debug.Log ("hit ground"); 
			}
			if (hit.collider.tag == "stairs") {
				groundy = hit.point.y;
				Debug.Log (groundy); 
			}

			// 0.02 was selected to account for floating point error
			// It is an arbitrary 
			//Debug.Log(Time.deltaTime); 
			if (Parent.position.y <= groundy - 0.01) {			 				
				//float pos = Mathf.Lerp(parent.position.y, groundy, Time.deltaTime); 
				Parent.transform.position = new Vector3(0,  Mathf.Lerp(Parent.position.y, groundy,  speed), 0); 
			} 
			else if (Parent.position.y > groundy + 0.01) {
				//parent.transform.Translate (0, -groundy * speed* Time.deltaTime, 0); 
				//float pos = Mathf.Lerp(parent.position.y, -groundy, Time.deltaTime); 
				//parent.transform.Translate (0,  Mathf.Lerp(parent.position.y, -groundy,  Time.deltaTime), 0); 
				Parent.transform.position = new Vector3(0,  Mathf.Lerp(Parent.position.y, groundy,  speed), 0); 


			}				
		}



	}




}
