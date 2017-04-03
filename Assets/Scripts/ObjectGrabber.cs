using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour {

    public Transform grabPosition;
    public float moveToGrabPositionTime;
    bool grabbed;

    Transform grabbedObject;
    private float startTime;

    // the start position for grabbing in local space;
    private Vector3 startPosition;

    List<Transform> grabbableObjects;
    // Use this for initialization
	void Start () {
        grabbableObjects = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButtonDown("Grab") && !grabbed)
        {
            if(grabbableObjects.Count > 0)
            {
                if (grabbableObjects.Count == 1)
                    Grab(grabbableObjects[0]);
            }
        }	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "grabbable")
        {
            grabbableObjects.Add(col.transform);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "grabbable")
        {
            grabbableObjects.Remove(col.transform);
        }
    }

    void Grab(Transform grabbing)
    {
        startTime = Time.time;
        grabbing.parent = grabPosition;
        startPosition = grabbing.localPosition;
        grabbedObject = grabbing;
        grabbed = true;
        grabbing.gameObject.layer = LayerMask.NameToLayer("grabbed");
        PhysicsObject po = grabbing.GetComponent<PhysicsObject>();
        if (po != null)
            po.GrabbedObject();
        StartCoroutine("moveToGrabPosition");
    }

    IEnumerator moveToGrabPosition()
    {
        while (Time.time < startTime + moveToGrabPositionTime)
        {
            float t = (Time.time - startTime) / moveToGrabPositionTime;
            grabbedObject.localPosition = Vector3.Lerp(startPosition, Vector3.zero, t);
            yield return null;
        }

        grabbedObject.position = grabPosition.position;
    }

    public void ThrewObject()
    {
        grabbed = false;
        //grabbedObject = null;
    }

    public bool Grabbed() { return grabbed; }

    public PhysicsObject GrabbedObject() { return grabbedObject.GetComponent<PhysicsObject>(); }

}
