using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToTopGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GUI.depth = 0;
	}
	
	// Update is called once per frame
	void Update () {
		GUI.depth = 9999999;
	}
}
