using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(MeshCollider))]
public class TileMap : MonoBehaviour {
	private int MAP_WIDTH = 4;
	private int MAP_HEIGHT = 4;
	private float TILE_SIZE = 1.0f;

	// Use this for initialization
	void Start () {
		BuildMesh();
	}
	
	void BuildMesh () {
		int numTiles = MAP_WIDTH * MAP_HEIGHT;
		int numTris = numTiles * 2;
		int vsize_x = MAP_WIDTH + 1;
		int vsize_y = MAP_HEIGHT + 1;
		int numVerts = vsize_x * vsize_y;

		// Generate mesh data
		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[numTris * 3];

		for(int y=0; y < MAP_HEIGHT; y++) {
			for(int x=0; x < MAP_WIDTH; x++) {
				vertices[y * vsize_x + x] = new Vector3(x * TILE_SIZE, y * TILE_SIZE, 0);
				normals[y * vsize_x + x] = Vector3.up;
				uv[y * vsize_x + x] = new Vector2((float)x / vsize_x, (float)y / vsize_y);
			}
		}

		for(int y=0; y < MAP_HEIGHT; y++) {
			for(int x=0; x < MAP_WIDTH; x++) {
				int squareIndex = y * MAP_WIDTH + x;
				int triOffset = squareIndex * 6;

				triangles[triOffset]	 = y * vsize_x + x;
				triangles[triOffset + 1] = y * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 2] = y * vsize_x + x + vsize_x + 0;
				
				triangles[triOffset + 3] = y * vsize_x + x + 0;
				triangles[triOffset + 3] = y * vsize_x + x + 1;
				triangles[triOffset + 5] = y * vsize_x + x + vsize_x + 1;
			}
		}
		//
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		MeshFilter myMeshFilter = this.GetComponent<MeshFilter>();
		MeshRenderer myMeshRenderer = this.GetComponent<MeshRenderer>(); 
		MeshCollider myMeshCollider = this.GetComponent<MeshCollider>();

		myMeshFilter.mesh = mesh;
		myMeshCollider.sharedMesh = mesh;
	}
}
