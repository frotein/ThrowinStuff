using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour {

    ObjectGrabber grabber;
    Transform grabPosition;
    float length;
    Vector2 dir;
    float throwPower;
    public float maxPowerLength, minPowerLength;
    public float throwPowerScaler;
    bool throwNextFrame;

    // Use this for initialization
	void Start ()
    {
        grabber = GetComponent<ObjectGrabber>();
        grabPosition = grabber.grabPosition;
        length = Vector2.Distance(grabPosition.position, transform.position);
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveGrabLocation();	
        if(grabber.Grabbed())
        {
            PhysicsSystem.instance.ShowProjectileLine(grabber.GrabbedObject(), dir * throwPower * throwPowerScaler);
            if (Input.GetButtonDown("Throw"))
            {
                Throw();
            }


        }
	}

    void MoveGrabLocation()
    {
        Vector2 mousePos = Controls.GetWorldPosition();
        dir = (mousePos - ((Vector2)transform.position));
        throwPower = Mathf.Clamp(dir.magnitude, minPowerLength,maxPowerLength);
        dir = dir.normalized;
        grabPosition.position = transform.position + ((Vector3)(dir * length));
    }

    void Throw()
    {
        PhysicsObject po = grabber.GrabbedObject();
        PhysicsSystem.instance.ResetFrames();
        po.ApplyPower(dir * throwPower * throwPowerScaler);
        po.transform.parent = null;
        po.ThrewObject();
        grabber.ThrewObject();
        po.RestoreLayer();
    }
}
