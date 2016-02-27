using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {
    // Number of clouds
    public int numClouds = 2;
    // array of each cloud 
    public GameObject[] cloudPreFabs;

    //min pos of each cloud 
    public Vector3 cloudPosMin;

    // max pos of each cloud 
    public Vector3 cloudPosMax;

    //min scale of each cloud
    public float cloudScaleMin = 1f;

    //max scale
    public float cloudScaleMax = 5f;
    //speed adjuster 
    public float cloudSpeedMult = 0.5f;
    //reference to cloud images  
    public GameObject[] cloudInstances; 

    void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");

        GameObject cloud; 
        for(int index = 0; index<numClouds; index++)
        {
            //pick an int 0 - numCloud.length-1
            //Random.Range will  return the highest number 
            int preFabNum = Random.Range(0, cloudPreFabs.Length);

            cloud = Instantiate(cloudPreFabs[preFabNum]) as GameObject;

            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            // smaller clouds with smaller scaleU should be near the ground

            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);

            //Clouds further away
            cPos.z = 100 - 90 * scaleU;

            cloud.transform.position = cPos;
            //apply scale to cloud
            cloud.transform.localScale = Vector3.one * scaleVal;

            cloud.transform.parent = anchor.transform;

            //add cloud to our instances array
            cloudInstances[index] = cloud; 

           
        } 
    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //iterate though clouds that was created 
        foreach(GameObject cloud in cloudInstances)
        {
            // get cloud scale position 
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;

            // move cloud to the left 
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult; 

            // if cloud moved too far to the left rest to the far right 
            //Optimize trick to save memory
            if(cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x; 
            }
            cloud.transform.position = cPos; 

        }
	}
}
