using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot1 : MonoBehaviour{
    [Header("Set in Inspector")]

    public GameObject prefabProjectile;
    public float velocityMult = 10f;
    public GameObject projLinePrefab;

    [Header("Set Dynamically")]

    public GameObject launchPoint;
    public Vector3 launchPos; 
    public GameObject projectile; 
    public bool aimingMode;
    private Rigidbody projRB;
    

    void Awake() {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject; 
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    
    void OnMouseEnter(){
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }
    void OnMouseExit(){
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown(){
        // The player has pressed the mouse button while over Slingshot
        aimingMode = true;
        // Instantiate a Projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        // Start it at the launchPoint
        projectile.transform.position = launchPos;
        // Set it to isKinematic for now 
        projRB = projectile.GetComponent<Rigidbody>(); 
        projRB.isKinematic = true;
    }
    void Update(){
        // If Slingshot is not in aimingMode, don't run this code
        if (!aimingMode) return; 
        // Get the current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition; 
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );
        // Find the delta from the launchPos to the mousePos3D
        Vector3 mouseDelta = mousePos3D-launchPos;
        // Limit mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if ( Input.GetMouseButtonUp(0) ) { 
            // The mouse has been released
            aimingMode = false;
            projRB.isKinematic = false; 
            projRB.velocity = -mouseDelta * velocityMult;
            FollowCam1.POI = projectile;
            Instantiate<GameObject>(projLinePrefab, projectile.transform);
            projectile = null; 
            Prototype.SHOT_TAKEN();
        }
    }
}
