using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
	static public bool goalMet = false;

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Projectile") {
			Goal.goalMet = true;
			GameObject goal = GameObject.FindGameObjectWithTag("Goal");
			Color c = goal.GetComponent<Renderer> ().material.color;
			c.r = 255;
			c.b = 255;
			goal.GetComponent<Renderer>().material.color = c;
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
