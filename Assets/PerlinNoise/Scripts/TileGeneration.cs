using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileGeneration : MonoBehaviour {

	[SerializeField]
	NoiseMapGeneration noiseMapGeneration;

	[SerializeField]
	private MeshRenderer tileRenderer;

	[SerializeField]
	private MeshFilter meshFilter;

	[SerializeField]
	private MeshCollider meshCollider;

	[SerializeField]
	private float levelScale;

	[SerializeField]
	private TerrainType[] terrainTypes;

	[SerializeField]
	private float heightMultiplier;

	[SerializeField]
	private AnimationCurve heightCurve;

    [SerializeField]
    private Wave waves;

	public NavMeshSurface surface;

	void Start() {
		waves.seed = Random.Range(0, 9999);
		GenerateTile ();
	}

    void GenerateTile()
    {
        // calculate tile depth and width based on the mesh vertices
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;

        // calculate the offsets based on the tile position
        float offsetX = -this.gameObject.transform.position.x;
        float offsetZ = -this.gameObject.transform.position.z;

        // generate a heightMap using noise
        float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, waves);

        // build a Texture2D from the height map
        Texture2D tileTexture = BuildTexture(heightMap);
        this.tileRenderer.material.mainTexture = tileTexture;

        // update the tile mesh vertices according to the height map
        UpdateMeshVertices(heightMap);


		surface.BuildNavMesh();
    }

	private Texture2D BuildTexture(float[,] heightMap) {
		int tileDepth = heightMap.GetLength (0);
		int tileWidth = heightMap.GetLength (1);

		Color[] colorMap = new Color[tileDepth * tileWidth];
		for (int zIndex = 0; zIndex < tileDepth; zIndex++) {
			for (int xIndex = 0; xIndex < tileWidth; xIndex++) {
				// transform the 2D map index is an Array index
				int colorIndex = zIndex * tileWidth + xIndex;
				float height = heightMap [zIndex, xIndex];
				// choose a terrain type according to the height value
				TerrainType terrainType = ChooseTerrainType (height);
				// assign the color according to the terrain type
				colorMap[colorIndex] = terrainType.color;
			}
		}

		// create a new texture and set its pixel colors
		Texture2D tileTexture = new Texture2D (tileWidth, tileDepth);
		tileTexture.wrapMode = TextureWrapMode.Clamp;
		tileTexture.SetPixels (colorMap);
		tileTexture.Apply ();

		return tileTexture;
	}

	TerrainType ChooseTerrainType(float height) {
		// for each terrain type, check if the height is lower than the one for the terrain type
		foreach (TerrainType terrainType in terrainTypes) {
			// return the first terrain type whose height is higher than the generated one
			if (height < terrainType.height) {
				return terrainType;
			}
		}
		return terrainTypes [0];
	}

	private void UpdateMeshVertices(float[,] heightMap) {
		int tileDepth = heightMap.GetLength (0);
		int tileWidth = heightMap.GetLength (1);

		Vector3[] meshVertices = this.meshFilter.mesh.vertices;

		// iterate through all the heightMap coordinates, updating the vertex index
		int vertexIndex = 0;
		for (int zIndex = 0; zIndex < tileDepth; zIndex++) {
			for (int xIndex = 0; xIndex < tileWidth; xIndex++) {
				float height = heightMap [zIndex, xIndex];

				Vector3 vertex = meshVertices [vertexIndex];
				// change the vertex Y coordinate, proportional to the height value. The height value is evaluated by the heightCurve function, in order to correct it.
				meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);

				vertexIndex++;
			}
		}

		// update the vertices in the mesh and update its properties
		this.meshFilter.mesh.vertices = meshVertices;
		this.meshFilter.mesh.RecalculateBounds ();
		this.meshFilter.mesh.RecalculateNormals ();
		// update the mesh collider
		this.meshCollider.sharedMesh = this.meshFilter.mesh;
	}
}

[System.Serializable]
public class TerrainType {
	public string name;
	public float height;
	public Color color;
}
