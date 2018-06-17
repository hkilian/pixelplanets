using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ResourceCount {

	PlanetResources.Matter type;
	float abundance;

}

public class PlanetConfig {

	// Enums
	public enum PlanetType {Solid, Gas, Ice};  
	public enum SizeClassification {Small, Medium, Large, Giant};  

	// Name
	public string name;

	// Type
	public PlanetType type;

	// Population
	public bool has_civ;
	public float density;
	public int population;

	// Size
	public float diameter;
	public SizeClassification size;

	// Atmosphere
	public float temperature;
	public float humidity;

	// Terra Firma
	public ResourceCount[] resources;

	// Clouds
	public bool has_clouds;
	public float cloud_elevation;
	public float cloud_coverage;

	// Meteorites
	public int num_rings = 0;
	public float ring_spacer = 0;
	public int num_meteorites = 0;
	public float meteorite_min_dis = 0;
	public float meteorite_max_dis = 0;
	public float meteorite_speed = 0;
	public float meteorite_speed_diff = 0;

	public float meteorite_size = 0;
	public float meteorite_size_variation = 0.0f;

	// Colors
	public Color l1_col;
	public Color l2_col;
	public Color l3_col;
	public Color l4_col;

	public Color nightlights_col;

	public static T GetRandomEnum<T>() {
		System.Array A = System.Enum.GetValues(typeof(T));
		T V = (T)A.GetValue(UnityEngine.Random.Range(0,A.Length));
		return V;
	}

}
