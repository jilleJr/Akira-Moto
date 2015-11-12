﻿using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {

	[SceneDropDown]
	public string nextLevel;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Application.LoadLevel(nextLevel);
		}
	}
}
