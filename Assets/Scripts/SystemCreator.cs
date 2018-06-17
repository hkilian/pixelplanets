using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCreator : MonoBehaviour {

	//// Max amount of systems
	private int max_systems = 999999;

	//// Seed for this system
	private int seed;

	//// Solar system info
	private int planet_count;
	private int[] planet_seeds;

	// PLanet limits class
	PlanetLimits p_limits;

	// Planet resources
	PlanetResources p_resources;

	// Use this for initialization
	void Start () {

		// Create limits object
		p_limits = new PlanetLimits();

		// Set planet resources object to the one createdin editor
		p_resources = GameObject.Find("Resources").GetComponent(typeof(PlanetResources)) as PlanetResources;

		// Create the initial system
		CreateSystem();
		
	}

	// Create system
	public void CreateSystem() {

		// Create new random seed
		seed = (int)Random.Range(0.0f, (float)max_systems);

		// Set random seed
		Random.InitState(seed);

		// Get number of planets
		planet_count = Random.Range(1, 10);

		// Set planet seeds
		planet_seeds = new int[planet_count];

		for(var i = 0; i < planet_count; i++) {
			planet_seeds [i] = (int)(Random.value * max_systems);
		}
			
		// Look for any existing planetry systems
		GameObject planet_system;
		planet_system = GameObject.Find("PlanetSystem(Clone)");

		// If found an existing planet then delete
		if (planet_system != null) {
			Destroy (planet_system);
		}

		// Create planet
		CreatePlanet (planet_seeds[0]);

	}

	// Update is called once per frame
	void CreatePlanet(int _seed) {

		// Create planet system
		GameObject planet_system = (GameObject)Instantiate (Resources.Load ("PlanetSystem"));
		PlanetarySystem psc = planet_system.GetComponent (typeof(PlanetarySystem)) as PlanetarySystem;

		// Planet
		PlanetConfig p_config = new PlanetConfig ();

		// Name
		p_config.name = "p" + seed.ToString();

		// Size
		p_config.diameter = Random.Range (p_limits.diameter_min_size, p_limits.diameter_max_size);
		p_config.size = ClassifySize(p_config.diameter);

		// Atmosphere
		p_config.temperature = Random.Range(p_limits.temperature_min, p_limits.temperature_max);
		p_config.humidity = Random.Range(p_limits.humidity_min, p_limits.humidity_max);

		// Classification (type)
		if (p_config.size >= PlanetConfig.SizeClassification.Large) {
			
			p_config.type = PlanetConfig.PlanetType.Gas;

		} else {
			
			p_config.type = PlanetConfig.PlanetType.Solid;

		}

		// Population (if planet solid)
		if (p_config.type == PlanetConfig.PlanetType.Solid) {
			
			p_config.has_civ = true;

			if (p_config.has_civ == true) {
				p_config.density = Random.Range (0.01f, p_limits.max_density);

				int max_pop = (int)((p_config.density * 1000) * 1.0 + (float)p_config.size) * 10000;
				int min_pop = (int)((p_config.density * 100) * 1.0 + (float)p_config.size) * 1000;

				p_config.population = Random.Range (min_pop, max_pop);

			} else {
				p_config.density = 0.0f;
				p_config.population = 0;
			}

		} else {
			p_config.has_civ = false;
			p_config.density = 0.0f;
			p_config.population = 0;
		}

		if (p_config.type == PlanetConfig.PlanetType.Solid) {
			
			// Clouds
			p_config.has_clouds = true;
			p_config.cloud_elevation = (4 - (float)p_config.size) / 5;
			p_config.cloud_coverage = Random.Range (0, 1.0f);

		} else {
			
			p_config.has_clouds = false;

		}

		// Set Meteorites
		SetResources(ref p_config);

		// Set Meteorites
		SetMeteorites(ref p_config);

		// Set nightlights color
		p_config.nightlights_col = new Color(0.8f, 0.6f, 0.1f);

		// Initiate planet
		psc.Initiate (_seed, p_config);

		// Update stats in GUI
		StatManager s_manager = GameObject.Find("UiUpdater").GetComponent(typeof(StatManager)) as StatManager;
		s_manager.SetStats (p_config);

	}

	void SetResources(ref PlanetConfig _p_config) {

		if (_p_config.type == PlanetConfig.PlanetType.Solid) {
			
			// Set Land color (l1)
			_p_config.l1_col = p_resources.m_resources [(int)PlanetResources.Matter.Vegetation].color;

			// Set Ocean color (l2)
			_p_config.l2_col = p_resources.m_resources [(int)PlanetResources.Matter.Water].color;

			// Set Sand color (l3)
			_p_config.l3_col = p_resources.m_resources [(int)PlanetResources.Matter.Sand].color;

		} else if (_p_config.type == PlanetConfig.PlanetType.Gas) {

			Color incol = new Color (0.1f, 0.1f, 0.1f);

			// Set Land color (l1)
			_p_config.l1_col = p_resources.g_resources [(int)PlanetResources.Gas.Methane].color;

			// Set Ocean color (l2)
			_p_config.l2_col = p_resources.g_resources [(int)PlanetResources.Gas.Methane].color + incol;

			// Set Sand color (l3)
			_p_config.l3_col = p_resources.g_resources [(int)PlanetResources.Gas.Methane].color + incol + incol;

		}


	}

	void SetMeteorites(ref PlanetConfig _p_config) {

		// Meteorites
		_p_config.num_rings = (int)Random.Range (1, p_limits.max_rings);

		if (_p_config.num_rings != 0) {

			_p_config.ring_spacer = Random.Range (p_limits.min_ring_spacer, p_limits.max_ring_spacer);

			_p_config.num_meteorites = (int)Random.Range (p_limits.min_meteorites, p_limits.max_meteorites) * (1 + (int)_p_config.size/2);

			// Set new ring limits based on size
			float min_ring_distance = (float)(_p_config.diameter / 2) + p_limits.min_ring_distance;
			float max_ring_distance = (float)(_p_config.diameter / 2) + p_limits.max_ring_distance;

			_p_config.meteorite_min_dis = min_ring_distance;
			_p_config.meteorite_max_dis = max_ring_distance;

			_p_config.meteorite_speed = Random.Range (p_limits.meteorite_min_speed, p_limits.meteorite_max_speed);
			_p_config.meteorite_speed_diff = Random.Range (p_limits.meteorite_speed_diff_min, p_limits.meteorite_speed_diff_max);

			_p_config.meteorite_size = Random.Range (p_limits.meteorite_min_size - (int)_p_config.size/20, p_limits.meteorite_max_size);


			// Decrese meteorite_size the larger the planet
			_p_config.meteorite_size = _p_config.meteorite_size / (1 + ((float)_p_config.size / 5));

			float div = (1 + ((float)_p_config.size / 5));


			_p_config.meteorite_size_variation = Random.Range (0, _p_config.meteorite_size / 2);

		} 

	}

	PlanetConfig.SizeClassification ClassifySize (float _size) {

		if (_size < p_limits.small_planet_limit) {
			 return PlanetConfig.SizeClassification.Small;
		} else if (_size < p_limits.medium_planet_limit) {
			return PlanetConfig.SizeClassification.Medium;
		} else if (_size < p_limits.large_planet_limit) {
			return PlanetConfig.SizeClassification.Large;
		} else {
			return PlanetConfig.SizeClassification.Giant;
		}

	}

	// Update is called once per frame
	void Update () {
		
	}

}
