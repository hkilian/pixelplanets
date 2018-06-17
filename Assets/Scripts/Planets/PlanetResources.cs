using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MResource {

	public PlanetResources.Matter type;
	public Color color;

}

public struct GResource {

	public PlanetResources.Gas type;
	public Color color;

}

public class PlanetResources : MonoBehaviour {

	// Public colors to make avalible for editor
	public Color col_soil;
	public Color col_sand;
	public Color col_water;
	public Color col_vegetation;

	public Color col_methane;

	// Resource matter types
	public enum Matter {Soil, Water, Vegetation, Sand, Ice, Sulfar, Iron, Copper, Uranium}; 

	// Resource gas types
	public enum Gas {Ammonia, Methane, Hydrogen, Helium}; 

	// Array of matter resources
	public MResource[] m_resources;

	// Array of gas resources
	public GResource[] g_resources;

	// Called at init
	void Start() {

		//// MATTER
		// Matters in game
		int matter_count = System.Enum.GetNames(typeof(PlanetResources.Matter)).Length;

		// Set resouces to matter count
		m_resources = new MResource[matter_count];

		// Loop through and set types
		for (int i = 0; i < matter_count; i++) {
			m_resources [i].type = (PlanetResources.Matter)i;
		}

		// Soil
		m_resources[(int)Matter.Soil].color = col_soil;

		// Water
		m_resources[(int)Matter.Water].color = col_water;

		// Sand
		m_resources[(int)Matter.Sand].color = col_sand;

		// Vegetation
		m_resources[(int)Matter.Vegetation].color = col_vegetation;

		//// GAS
		int gas_count = System.Enum.GetNames(typeof(PlanetResources.Gas)).Length;

		// Set resouces to matter count
		g_resources = new GResource[matter_count];

		// Loop through and set types
		for (int i = 0; i < matter_count; i++) {
			g_resources [i].type = (PlanetResources.Gas)i;
		}

		// Methane
		g_resources[(int)Gas.Methane].color = col_methane;

	}
}
