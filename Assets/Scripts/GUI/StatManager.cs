using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour {

	// Store GUI text elements
	public Text planet_name;
	public Text population;
	public Text size;

	// Use this for initialization
	void Start () {

		planet_name = GameObject.Find("NameStat").GetComponent(typeof(Text)) as Text;
		population = GameObject.Find("PopulationStat").GetComponent(typeof(Text)) as Text;
		size = GameObject.Find("SizeStat").GetComponent(typeof(Text)) as Text;

	}

	public void SetStats(PlanetConfig _p_config) {

		planet_name.text = "NAME: " +  _p_config.name.ToUpper();
		population.text = "POPULATION: " + _p_config.population;
		size.text = "SIZE: " + _p_config.size.ToString().ToUpper() + " (" + (_p_config.diameter) + ")";

	}
	

}
