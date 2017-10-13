using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour {
    public SteamVR_Controller.Device controller {get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }
    public SteamVR_TrackedObject trackedObj;
    private RaycastHit hit;
    Vector3 dwd;
    float groundy; // keep track of the y value of the ground beneath the player

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        groundy = 0;
        dwd = new Vector3(0, -1, 0);
        return;
    }
	

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Physics.Raycast(transform.position, dwd, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if (hit.collider.tag == "ground")
            {
                Debug.Log("hit ground");
            }
            if (hit.collider.tag == "stairs")
            {
                groundy = hit.point.y;
                Debug.Log(groundy);
            }
           
        }



    }
}
