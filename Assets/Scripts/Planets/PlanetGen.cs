using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccidentalNoise;

public class PlanetGen : MonoBehaviour {
	
	// Update is called once per frame
	public void GeneratePlanet(PlanetConfig _p_config) {

		// Create heightmap
		CreateHeightMap (_p_config);

		
	}

	private void CreateHeightMap(PlanetConfig _p_config) {

		Debug.Log ("Creating planet heightmap", gameObject);

		//// Create base map
		TextureGen base_gen = new TextureGen();
		base_gen.frequency = Random.Range ((float)_p_config.size, 1.0f + (float)_p_config.size + (float)_p_config.size * 2);

		// Set the texture
		GetComponent<Renderer>().material.SetTexture("_DiffuseTexture", base_gen.getTexture());

		//// Create civ map
		CivTexGen civ_gen = new CivTexGen();
		GetComponent<Renderer>().material.SetTexture("_CivTexture", civ_gen.getTexture(base_gen.getTexture(), _p_config.has_civ, _p_config.density));

	}
		



}
