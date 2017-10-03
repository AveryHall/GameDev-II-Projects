using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {
    // fields set in the Unity Inspector pane
    public GameObject prefabProjectile;
    public float velocityMult = 4f;
    public bool _____________;  
    // fields sets dynamically
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private void Awake() {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;

    }

    void OnMouseEnter() {
        //print("SlingShot: OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit() {
        //print("Slingshot: OnMouseExit()");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown() {
        // The player has pressed the mouse button while over Slingshot
        aimingMode = true;
        // Instantiate a Projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        // Start it at launchPoint
        projectile.transform.position = launchPos;
        // Set it to is kinematic for now
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // If Slingshot is not in aimingMode, don't run this code
        if (!aimingMode) return;
        //Get the current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        //Convert the mouse position to 3dD world coordinates
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
	}
}
