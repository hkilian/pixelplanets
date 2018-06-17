using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivTexGen {

	private Texture2D civ_map;

	public int seed = 0;
	public int texture_width = 512;

	// Use this for initialization
	public CivTexGen() {



	}

	public Texture2D getTexture(Texture2D _heightmap, bool _haslife, float _density) {

		// Create civ texture
		civ_map = new Texture2D(texture_width, texture_width);
		civ_map.filterMode = FilterMode.Point;

		// Fill heightmap
		for(var i = 0; i < (texture_width * texture_width); i++) {

			int x = i / texture_width;
			int y = i % texture_width;

			Color col =  Color.black;

			if (_haslife == true) {
				
				if (_heightmap.GetPixel (x / 4, y / 4).r > 0.5) {

					if (Random.value * 10 < _density) {
						col = Color.white;
					} else if (Random.value * 20 < _density) {
						col = Color.gray;
					}

				}

			}
				
			civ_map.SetPixel(x, y, col);

		}

		civ_map.Apply();

		return civ_map;

	}

}