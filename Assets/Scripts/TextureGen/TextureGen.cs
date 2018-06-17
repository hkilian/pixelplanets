using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccidentalNoise;

public class TextureGen {

	private Texture2D heightmap;

	public int seed = 0;
	public int texture_width = 128;

	public float noise_scale = 1.0f;
	public float sea_level = 0.5f;

	public FractalType fractalType = FractalType.RIDGEDMULTI;
	public BasisTypes basisType = BasisTypes.GRADIENT;
	public InterpTypes interpType = InterpTypes.QUINTIC;

	public int octaves = 6;
	public double frequency = 2.0;
	public double lacunarity = 2.0;

	// Use this for initialization
	public TextureGen() {


		
	}

	public Texture2D getTexture() {

		Fractal fractal = new Fractal(fractalType, basisType, interpType, octaves, frequency, null);
		fractal.SetLacunarity(lacunarity);
		fractal.seed = seed;

		// Create new texture
		heightmap = new Texture2D(texture_width, texture_width);

		SMappingRanges ranges = new SMappingRanges();
		ranges.loopx0 = 1.0;
		ranges.loopy0 = 1.0;
		ranges.loopz0 = 1.0;

		// Fill heightmap
		for(var i = 0; i < (texture_width * texture_width); i++) {

			int x = i / texture_width;
			int y = i % texture_width;

			float noise_x = ((float)x / (float)texture_width) * noise_scale;
			float noise_y = ((float)y / (float)texture_width) * noise_scale;

			double nx = ranges.mapx0 + noise_x * (ranges.mapx1 - ranges.mapx0);
			double ny = ranges.mapy0 + noise_y * (ranges.mapy1 - ranges.mapy0);

			float sample = (float)fractal .Get(nx, ny);

			if (sample >= sea_level) {
				//sample = sample;
			} else {
				//sample  -= 0.2f;
			}

			Color col = new Color(sample, sample, sample);

			heightmap.SetPixel(x, y, col);

		}

		heightmap.Apply();

		return heightmap;

	}

}
