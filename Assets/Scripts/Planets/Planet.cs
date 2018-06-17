using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	// Planet Config
	public float axis = 20;
	public float rotation_speed = 25;
	public float temperature;
	public float sea_level;

	// Fixed update
	void FixedUpdate() {

		// Rotate planet
		transform.Rotate (Vector3.up * Time.deltaTime * rotation_speed);

	}

	// Generate new planet
	public void generatePlanet(PlanetConfig _p_config) {

		// Add the planet gen componant
		PlanetGen gen = gameObject.AddComponent<PlanetGen>() as PlanetGen;
		gen.GeneratePlanet (_p_config);

		// Set material colors in shader
		GetComponent<Renderer>().material.SetColor("LandCol", _p_config.l1_col);
		GetComponent<Renderer>().material.SetColor("OceanCol", _p_config.l2_col);
		GetComponent<Renderer>().material.SetColor("SandCol", _p_config.l3_col);
		GetComponent<Renderer>().material.SetColor("MountainsCol", _p_config.l4_col);
		GetComponent<Renderer>().material.SetColor("NightLightsCol", _p_config.nightlights_col);

	}

}
