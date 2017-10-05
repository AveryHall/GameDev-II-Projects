using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
	static public FollowCam S; // a FollowCam Singleton

	// fields set in the Unity Inspector pane
	public float easing = 0.05f;
	public Vector2 minXY;
	public bool _____________________________;

	// fields set dynamically
	public GameObject poi; // That is, the point of interest
	public float camZ; // The desired Z pos of the camera

	void Awake() {
		
		S = this;
		camZ = this.transform.position.z;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (poi == null)
			return; //return if there is no poi
		
		// Get the position of the poi
		Vector3 destination = poi.transform.position;

		//Limit the X & Y to minimum values
		destination.x = Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max(minXY.y, destination.y);

		// Interpolate from the current Camera position toward destination
		destination = Vector3.Lerp(transform.position, destination, easing);

		//Retain the destination.z of camZ
		destination.z = camZ;

		// Set the camera to the destination
		transform.position = destination;

		// Set the orthographicSize of the Camera to keep the Ground in view
		// the orginal line was this.camera.orthgraphicSize = destination.y + 10;
		this.GetComponent<Camera>().orthographicSize = destination.y + 10;
	}
}
