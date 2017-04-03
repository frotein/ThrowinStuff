using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {


    public float mass;
    Controller2D controller;
    public Vector2 velocity;
    public bool grabbed;

    string originalLayer;
    // Use this for initialization
    void Start ()
    {
        
        controller = GetComponent<Controller2D>();
        originalLayer = LayerMask.LayerToName(gameObject.layer);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!grabbed)
            PhysicsStep();
        else
            velocity = Vector2.zero;
	}

    void PhysicsStep()
    {
        PhysicsSystem.instance.PhysicsStep(this);
    }

    public bool OnGround()
    {
        return controller.collisions.below;
    }

    public Controller2D.CollisionInfo CollisionInfo()
    {
        return controller.collisions;
    }

    public void Move()
    {
        controller.Move(velocity * Time.deltaTime);
    }

    public void GrabbedObject()
    {
        grabbed = true;
    }

    public void ApplyPower(Vector2 powerAndDir)
    {
        velocity = powerAndDir / mass;
    }

    public void ThrewObject()
    {
        grabbed = false;
    }

    public void RestoreLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(originalLayer);
    }

}
