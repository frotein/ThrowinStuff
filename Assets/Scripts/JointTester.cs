using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointTester : MonoBehaviour {

    MyDistanceJoint2D joint;
    // Use this for initialization
	void Start ()
    {
        joint = transform.GetComponent<MyDistanceJoint2D>();
        joint.anchorPoint = (Vector2)transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
        joint.anchorPoint = (Vector2)transform.position;
    }
}
