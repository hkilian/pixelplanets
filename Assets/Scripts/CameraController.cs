using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float move_speed = 10.0f;


	private Vector3 world_center;

	// Use this for initialization
	void Start () {

		world_center = new Vector3 (0.0f, 0.0f, 0.0f);

		Debug.Log ("Setting world center", gameObject);

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Fixed update
	void FixedUpdate() {

		transform.LookAt(world_center);
		transform.Translate(Vector3.right * Time.deltaTime * move_speed);


	}
}
