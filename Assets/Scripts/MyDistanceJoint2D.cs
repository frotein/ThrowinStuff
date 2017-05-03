using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// only works for player now, could make better infrastructure for movable object so it works with them
public class MyDistanceJoint2D : MonoBehaviour
{

    public float maxDistance;
    public Vector2 anchorPoint;
    public Player connectedPlayer;
    public Transform testPoint;
    float distSqr;
    // Use this for initialization
	void Start ()
    {
        //distSqr = distance * distance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ApplyLength();
	}

    public void ApplyLength()
    {
        if(BeyondLength())
        {
            Vector2 playerPos = connectedPlayer.NextFramePosition();
            Vector2 dir = (anchorPoint - playerPos).normalized;
            Vector2 newPoint = anchorPoint + -dir * maxDistance;

            //Vector2 newMovement = newPoint - playerPos;

            //connectedPlayer.AdjustVelocity(newMovement);

            connectedPlayer.transform.position = newPoint.XYZ(connectedPlayer.transform.position.z);
            bool left = ExtensionMethods.isLeft(newPoint, anchorPoint, playerPos);


            //testPoint.position = newPoint + perpDir * 5f;

            Debug.Log(perpDir.x);
            
        }
    }

    bool BeyondLength()
    {
        float dist = Vector2.Distance(anchorPoint, connectedPlayer.NextFramePosition());
       // Debug.Log(dist);
        return dist > maxDistance;
    }
}
