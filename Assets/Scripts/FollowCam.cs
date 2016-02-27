using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
    static public FollowCam S;
    //Point of interest 
    public GameObject poi;

    public float camZ;
    public float easing = 0.05f;
    public Vector2 minXY; 

   
    /// <summary>
    /// Because S is a public static the singleton S can be accessed 
    /// anywhere in our project by calling FollowCam.S
    /// This allows us to set the public poi field from anywhere
    /// by setting followCam.S.poi
    /// </summary>

	// Use this for initialization
	void Awake () {
        S = this;
        camZ = this.transform.position.z; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 destination;
        if (poi == null)
        {
            destination = Vector3.zero;
            destination.z = camZ;
            transform.position = destination; 
        }
        else
        {
            //get position of poi
             destination = poi.transform.position;
            //limit the camerea x and y
            if(poi.tag == "projectile")
            {
                //if its sleeping 
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    poi = null;
                    return; 
                }
            }
            destination.x = Mathf.Max(minXY.x, destination.x);
            destination.y = Mathf.Max(minXY.y, destination.y);
            destination = Vector3.Lerp(transform.position, destination, easing);
            //
            destination.z = camZ;

            transform.position = destination;
            // set orthographic size of the camera
            this.GetComponent<Camera>().orthographicSize = destination.y + 10;
        }
	
	}
}
