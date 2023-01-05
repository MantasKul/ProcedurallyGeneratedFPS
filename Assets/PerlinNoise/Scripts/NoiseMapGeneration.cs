using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGeneration : MonoBehaviour {

	public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float scale, float offsetX, float offsetZ, Wave waves) {
		// create an empty noise map with the mapDepth and mapWidth coordinates
		float[,] noiseMap = new float[mapDepth, mapWidth];

		for (int zIndex = 0; zIndex < mapDepth; zIndex++) {
			for (int xIndex = 0; xIndex < mapWidth; xIndex++) {
				// calculate sample indices based on the coordinates, the scale and the offset
				float sampleX = (xIndex + offsetX) / scale;
				float sampleZ = (zIndex + offsetZ) / scale;

				float noise = 0f;
				float normalization = 0f;
				
					// generate noise value using PerlinNoise for a given Wave
					noise += waves.amplitude * Mathf.PerlinNoise (sampleX * waves.frequency + waves.seed, sampleZ * waves.frequency + waves.seed);
					normalization += waves.amplitude;
				
				// normalize the noise value so that it is within 0 and 1
				noise /= normalization;

				noiseMap [zIndex, xIndex] = noise;
			}
		}

		return noiseMap;
	}
}

[System.Serializable]
public class Wave {
	public float seed;
	public float frequency;
	public float amplitude;
}
