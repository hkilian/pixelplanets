using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLimits {

	// Meteorites
	public int max_rings = 4;

	public float min_ring_distance = 2;
	public float max_ring_distance = 5;

	public float min_ring_spacer = 1;
	public float max_ring_spacer = 3;

	public int min_meteorites = 10;
	public int max_meteorites = 150;

	public float meteorite_min_speed = 0.02f;
	public float meteorite_max_speed = 0.3f;

	public float meteorite_speed_diff_min = 1.0f;
	public float meteorite_speed_diff_max = 4.0f;

	public float meteorite_min_size = 0.15f;
	public float meteorite_max_size = 0.5f;

	// Planet
	public float diameter_min_size = 15;
	public float diameter_max_size = 25;

	public float small_planet_limit = 17;
	public float medium_planet_limit = 21;
	public float large_planet_limit = 23;

	// Atmosphere
	public float temperature_min = 15;
	public float temperature_max = 150;

	public float humidity_min = 0;
	public float humidity_max = 100;

	// Population
	public float max_density = 0.1f;

}
