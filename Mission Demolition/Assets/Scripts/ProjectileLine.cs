using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour {

	static public ProjectileLine S; // Singleton

	//fields set in the Unity Inspector pane
	public float minDist = 0.1f;
	public bool ____________________________;

	// Fields set dynamically
	public LineRenderer line;
	private GameObject _poi;
	public List<Vector3> points;

	void Awake () {

		S = this; // Set the Singleton

		//Get a reference to the LineRenderer
		line = GetComponent<LineRenderer>();

		//Disable the LineRenderer until it's needed
		line.enabled = false;

		//Initialize the points List
		points = new List<Vector3>();

	}

	//This is a property (i.e. a method acting as a field)
	public GameObject poi {
		get {
			return(_poi);
		}
		set {
			_poi = value;

			if (_poi != null) {
				//When _poi is set to something new, it resets everything
				line.enabled = false;
				points = new List<Vector3> ();
				AddPoint ();
			}
		}
	}

	//This can be done to clear the line directly
	public void Clear() {
		_poi = null;
		line.enabled = false;
		points = new List<Vector3> ();
	}

	public void AddPoint() {

		//This is called to add a point to the line
		Vector3 pt = _poi.transform.position;

		if (points.Count > 0 && (pt - lastPoint).magnitude < minDist) {
			return; // If the point isn't far enough from the last point, it returns
		}

		if (points.Count == 0) {

			//If this is the launch point
			Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
			Vector3 launchPosDiff = pt - launchPos;

			//It adds an extra bit of line to aid in aiming later
			points.Add (pt + launchPosDiff);
			points.Add (pt);
			line.positionCount = 2;

			//Sets the first two points
			line.SetPosition (0, points [0]);
			line.SetPosition (1, points [1]);

			//Enables the LineRenderer
			line.enabled = true;
		} else {

			//Normal behavior of adding a point
			points.Add(pt);
			line.positionCount = points.Count;
			line.SetPosition (points.Count - 1, lastPoint);
			line.enabled = true;
		}

	}

	//Returns the location of the most recently added point
	public Vector3 lastPoint {

		get{

			if (points == null) {
				
				return(Vector3.zero); //If there are no points return zero vector

			}

			return(points[points.Count-1]);

		}

	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (poi == null) {

			//If there is no poi, search for one
			if (FollowCam.S.poi != null) {
				if (FollowCam.S.poi.tag == "Projectile") {
					poi = FollowCam.S.poi;
				} else {
					return; //return if we didn't find a poi
				}
			} else {
				return; //return if we didn't find a poi
			}

		}

		//If there is a poi, it's loc is added to FixedUpdate
		AddPoint();

		if (poi.GetComponent<Rigidbody> ().IsSleeping ()) {

			//Once the poi is sleeping, it is cleared
			poi = null;

		}
		
	}
}
