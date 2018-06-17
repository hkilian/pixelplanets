using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNewSystem : MonoBehaviour {

	// Use this for initialization
	public void NewSystem () {

		Debug.Log ("Showing new system", gameObject);

		// Find system creator and trigger new system
		GameObject system_creator;
		system_creator = GameObject.Find("SystemCreator");

		if (system_creator != null) {

			// Get the systemcreator componant
			SystemCreator sc = system_creator.GetComponent (typeof(SystemCreator)) as SystemCreator;

			sc.CreateSystem();

		} else {
			Debug.Log ("Could not find system creator", gameObject);
		}
		
	}

}
