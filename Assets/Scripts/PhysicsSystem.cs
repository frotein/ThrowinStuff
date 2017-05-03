using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSystem : MonoBehaviour {

    public LineRenderer rend;
    public float gravity;
    public static PhysicsSystem instance;
    public float lineRefreshRate;
    public int ignoreFrames;

    int frame;
    bool hitSomething;
    float refreshTime = 0;
    int lastCount;
    // Use this for initialization
	void Start ()
    {
        instance = this;	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    void PhysicsStepEverything()
    {

    }

    public void PhysicsStep(PhysicsObject pObject)
    {
        pObject.velocity.y += gravity * Time.deltaTime;

        hitSomething = false;
        Controller2D.CollisionInfo colInfo = pObject.CollisionInfo();

        if (frame >= ignoreFrames)
        {
            if (colInfo.above || colInfo.below)
            { pObject.velocity.y = 0; hitSomething = true; }


            if (colInfo.left || colInfo.right || colInfo.below)
            { pObject.velocity.x = 0; hitSomething = true; }
        }
        else frame++;

        pObject.Move();
    }

    public void ShowProjectileLine(PhysicsObject pObject, Vector2 powerAndDir)
    {
        Vector3 startPosition = pObject.transform.position;
        pObject.ApplyPower(powerAndDir);
        int breakout = 0;
        List<Vector3> positions = new List<Vector3>();
        positions.Add(startPosition);
        hitSomething = false;
        frame = 0;
        while(!hitSomething && breakout < 500)
        {
            PhysicsStep(pObject);
            breakout++;
            positions.Add(pObject.transform.position);
        }

        //Debug.Log(positions.Count);
        
        if (Time.time > refreshTime + lineRefreshRate && lastCount - positions.Count < 10)
        {
            rend.positionCount = (positions.Count);
            rend.SetPositions(positions.ToArray());
            refreshTime = Time.time;
        }


        lastCount = positions.Count;
        pObject.transform.position = startPosition;
    }

    public void ResetFrames()
    {
        frame = 0;
    }
}
