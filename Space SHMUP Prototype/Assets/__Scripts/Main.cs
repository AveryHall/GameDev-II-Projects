﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	
	static public Main S;
	static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;

	public GameObject[]     prefabEnemies;
	public float            enemySpawnPerSecond = 0.5f; // # Enemies/second
	public float            enemySpawnPadding = 1.5f; // Padding for position
	public WeaponDefinition[] weaponDefintions;

	public bool ________________;

	public WeaponType[] activeWeaponTypes;
	public float            enemySpawnRate; // Delay between Enemy spawns

	void Awake() {
		
		S = this;

		// Set Utils.camBounds
		Utils.SetCameraBounds(this.GetComponent<Camera>());

		// 0.5 enemies/second = enemySpawnRate of 2
		enemySpawnRate = 1f/enemySpawnPerSecond;                            // 1

		// Invoke call SpawnEnemy() once after a 2 second delay
		Invoke( "SpawnEnemy", enemySpawnRate );                             // 2

		// A generic Dictionary with WeaponType as the key
		W_DEFS = new Dictionary<WeaponType, WeaponDefinition>();
		foreach (WeaponDefinition def in weaponDefintions) {
			W_DEFS [def.type] = def;
		}

	}


	static public WeaponDefinition GetWeaponDefinition (WeaponType wt) {

		// Check to make sure that the key exists in the Dictionary
		// Attempting to retrieve a key that didn't exist, would throw an error,
		//   so the following if statement is important.
		if(W_DEFS.ContainsKey(wt)) {
			return (W_DEFS[wt]);
		}
		
		//This will return a definition for WeaponType.none,
		// which means it has failed to find the WeaponDefinition
		return( new WeaponDefinition());

	}

	void Start() {
		
		activeWeaponTypes = new WeaponType[weaponDefintions.Length];
		for (int i = 0; i < weaponDefintions.Length; i++) {
			activeWeaponTypes [i] = weaponDefintions [i].type;
		}

	}

	public void SpawnEnemy() {
		
		// Pick a random Enemy prefab to instantiate
		int ndx = Random.Range(0, prefabEnemies.Length);
		GameObject go = Instantiate( prefabEnemies[ ndx ] ) as GameObject;

		// Position the Enemy above the screen with a random x position
		Vector3 pos = Vector3.zero;
		float xMin = Utils.camBounds.min.x+enemySpawnPadding;
		float xMax = Utils.camBounds.max.x-enemySpawnPadding;
		pos.x = Random.Range( xMin, xMax );
		pos.y = Utils.camBounds.max.y + enemySpawnPadding;
		go.transform.position = pos;

		// Call SpawnEnemy() again in a couple of seconds
		Invoke( "SpawnEnemy", enemySpawnRate );                             // 3
	}

	public void DelayedRestart(float delay) {
		//Invoke the Restart() method in delay seconds
		Invoke("Restart", delay);
	}

	public void Restart() {
		// Reload _Scene_0 to restart the game
		Application.LoadLevel("__Scene_0");
	}

}