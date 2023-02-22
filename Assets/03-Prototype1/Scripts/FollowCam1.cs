using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam1 : MonoBehaviour{
    static private FollowCam1 S;
    static public GameObject POI; 

    public enum eView{none, slingshot, castle, both};

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    public GameObject viewBothGO;

    [Header("Set Dynamically")]
    public float camZ; 
    public eView nextView = eView.slingshot;

    void Awake() {
        camZ = this.transform.position.z;
        S = this;
    }
    void FixedUpdate () {

       // if (POI == null) return;
        // Get the position of the poi
        //Vector3 destination = POI.transform.position;
        Vector3 destination = Vector3.zero;
        if(POI != null){
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if((poiRigid != null) && poiRigid.IsSleeping()){
                POI = null;
            }
        }
        if(POI != null){
            destination = POI.transform.position;
        }
        destination.x = Mathf.Max( minXY.x, destination.x );
        destination.y = Mathf.Max( minXY.y, destination.y );
         
        destination = Vector3.Lerp(transform.position, destination, easing);

        destination.z = camZ;
        // Set the camera to the destination
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 15; 
    }
   
    
    
}
