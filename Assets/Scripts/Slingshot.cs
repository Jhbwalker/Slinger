using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	//member variables to be set in Inspector
	public GameObject launchPoint;
	public GameObject prefabProjectile;

	public bool ___________________________________________;

	//member variables to be set dynamically (code)
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;
    public float velocityMult = 4f; 

	//need awake() to initialize launchpoint
	void Awake(){
		Transform launchPointTrans = transform.Find ("launchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter(){
		//print ("Slingshot:OnMouseEnter");
		launchPoint.SetActive (true);
	}

	void OnMouseExit(){
		//print ("Slingshot:OnMouseExit");
		launchPoint.SetActive (false);
	}

	void OnMouseDown(){
		//Player has pressed the mouse button while over the slingshot
		aimingMode = true;

		//instantiate projectile
		projectile = Instantiate (prefabProjectile) as GameObject;

		//start projectile at our launch poisition
		projectile.transform.position = launchPos;

		//Set it to isKinematic for now
		projectile.GetComponent<Rigidbody>().isKinematic = true;
	}
    void Update()
    {
        if (!aimingMode)
        {
            return;
        }

        Vector3 MousePosition2D = Input.mousePosition;
        MousePosition2D.z = -Camera.main.transform.position.z;
        Vector3 MousePos3D = Camera.main.ScreenToWorldPoint(MousePosition2D);
        Vector3 mouseDelta = MousePos3D - launchPos;

        float maxMag = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMag)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMag;

        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            FollowCam.S.poi = projectile; 
            projectile = null;
        }

    }
}
