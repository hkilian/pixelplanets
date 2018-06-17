using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccidentalNoise;

public class PlanetClouds : MonoBehaviour {

	public float rotation_speed = 20;

	public float frequency = 2.0f;

	// Use this for initialization
	void Start () {

		// Create base map
		TextureGen base_gen = new TextureGen();
		base_gen.texture_width = 40;
		base_gen.fractalType = FractalType.RIDGEDMULTI;
		base_gen.frequency = Random.Range (2.0f, 3.0f);

		// Set the texture
		GetComponent<Renderer>().material.SetTexture("_DiffuseTexture", base_gen.getTexture());
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// Rotate planet
		transform.Rotate (Vector3.up * Time.deltaTime * rotation_speed);

		
	}
}
