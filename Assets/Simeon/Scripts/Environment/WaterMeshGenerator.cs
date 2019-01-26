using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMeshGenerator : MonoBehaviour
{
    public float size = 1;
    public int gridsize = 16;

    MeshFilter filter;

    void Start()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
    }

    Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        var verticies = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        for (int x = 0; x < gridsize + 1; x++)
        {
            for (int y = 0; y < gridsize + 1; y++)
            {
                verticies.Add(new Vector3(-size * 0.5f + size * (x / ((float)gridsize)), 0, -size * 0.5f + size * (y / ((float)gridsize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)gridsize, y / (float)gridsize));
            }
        }

        var triangles = new List<int>();
        var vertCount = gridsize + 1;

        for (int i = 0; i < vertCount * vertCount - vertCount; i++)
        {
            if ((i + 1) % vertCount == 0)
            {
                continue;
            }
            triangles.AddRange(new List<int>(){
                i+1+vertCount, i+vertCount, i,
                i, i+1, i+vertCount+1
            });
        }

        mesh.SetVertices(verticies);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }
}
