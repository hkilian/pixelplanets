using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetarySystem : MonoBehaviour {

	//// Seed for this planet
	public int seed;

	//// Containers
	GameObject[] meteorites;

	// Config for this planet
	PlanetConfig p_config;

	// Used for sin cos
	float time = 0;

	// Use this for initialization
	public void Initiate(int _seed, PlanetConfig _p_config) {

		// Set the config
		p_config = _p_config;

		// Set random seed
		seed = _seed;
		Random.InitState(_seed);

		// Get planet object
		GameObject planet = this.gameObject.transform.Find("PlanetMain").gameObject;

		// Set size for planet
		float p_size = _p_config.diameter;
		planet.transform.localScale = new Vector3(p_size, p_size, p_size);

		// Create clouds and set size
		if (_p_config.has_clouds) {
			
			GameObject clouds = (GameObject)Instantiate (Resources.Load ("Clouds"));
			clouds.transform.parent = transform;

			// Set size relative to planet size
			float c_size = p_size + _p_config.cloud_elevation;
			clouds.transform.localScale = new Vector3(c_size, c_size, c_size);
		}

		// If we have at least one ring then create meteorites
		if ( _p_config.num_rings != 0) {
			// Set size of meteorites
			meteorites = new GameObject[_p_config.num_meteorites];

			// Create meteorites
			for (int i = 0; i < _p_config.num_meteorites; i++) {
				
				GameObject m = (GameObject)Instantiate (Resources.Load ("Meteorite"));
				m.transform.parent = transform;

				Meteorite meteorite = m.GetComponent (typeof(Meteorite)) as Meteorite;

				// Distance from planet
				float distance = _p_config.meteorite_min_dis + Random.Range (0.0f, _p_config.meteorite_max_dis - _p_config.meteorite_min_dis);

				// Move into ring
				if (_p_config.num_rings > 1) {

					int ring = (int)Mathf.Round(Random.value * _p_config.num_rings);
					meteorite.ring_id = ring;

					if (ring > 1) { distance += ring * _p_config.ring_spacer; }

				}

				// Set size
				float size = _p_config.meteorite_size + Random.Range(0.0f, _p_config.meteorite_size_variation);
				meteorite.transform.localScale = new Vector3(size, size, size);

				meteorite.distance_from_planet = distance;

				meteorite.pos = Random.Range (1.0f, 100.0f);

				meteorites [i] = m;

			}
		}

		// Generate planet
		Planet planet_c = planet.GetComponent (typeof(Planet)) as Planet;
		planet_c.generatePlanet(_p_config);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// Inc time
		time += Time.deltaTime * p_config.meteorite_speed;

		if (p_config.num_rings != 0) {
			for (int i = 0; i < p_config.num_meteorites; i++) {

				Meteorite meteorite = meteorites [i].GetComponent (typeof(Meteorite)) as Meteorite;

				float dis = (p_config.meteorite_max_dis) - meteorite.distance_from_planet;

				float t = meteorite.pos + (time * (dis + p_config.meteorite_speed_diff));

				float x = Mathf.Sin (t) * meteorite.distance_from_planet;
				float z = Mathf.Cos (t) * meteorite.distance_from_planet;

				meteorites [i].transform.localPosition = new Vector3 (x, 0, z);

			}
		}
		
	}
}
