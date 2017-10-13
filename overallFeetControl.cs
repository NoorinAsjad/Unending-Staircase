using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overallFeetControl : MonoBehaviour {
    public float hitLeft;
    public float hitRight;
    Transform Parent;
    float speed;


    // Use this for initialization
    void Start () {
        Parent = transform.parent;
        speed = 0.05f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (hitLeft < hitRight-0.01)
        {
            Parent.transform.position = new Vector3(0, Mathf.Lerp(Parent.position.y, hitLeft, speed), 0);
        }
        else if (hitLeft > hitRight+0.01)
        {
            Parent.transform.position = new Vector3(0, Mathf.Lerp(Parent.position.y, hitRight, speed), 0);
        }
        else
        {
            Parent.transform.position = new Vector3(0, Mathf.Lerp(Parent.position.y, hitLeft, speed), 0);
        }


       
	}
}
