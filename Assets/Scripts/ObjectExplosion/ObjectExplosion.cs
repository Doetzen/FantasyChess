using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExplosion : MonoBehaviour
{
    public int gridSize = 3;          // Number of subdivisions along each axis
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public float upwardModifier = 1f;

    public void Explode()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshFilter == null || meshRenderer == null)
        {
            Debug.LogError("Object must have a MeshFilter and MeshRenderer!");
            return;
        }

        Mesh originalMesh = meshFilter.mesh;
        Vector3[] vertices = originalMesh.vertices;
        int[] triangles = originalMesh.triangles;

        // Calculate bounds and cell size
        Bounds bounds = originalMesh.bounds;
        Vector3 cellSize = bounds.size / gridSize;

        // Create a dictionary to group triangles into spatial clusters
        Dictionary<Vector3Int, List<int>> clusters = new Dictionary<Vector3Int, List<int>>();

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Calculate the average position of the triangle
            Vector3 averagePosition = (vertices[triangles[i]] +
                                       vertices[triangles[i + 1]] +
                                       vertices[triangles[i + 2]]) / 3;

            // Determine which grid cell the triangle belongs to
            Vector3 localPosition = averagePosition - bounds.min;
            Vector3Int cellIndex = new Vector3Int(
                Mathf.FloorToInt(localPosition.x / cellSize.x),
                Mathf.FloorToInt(localPosition.y / cellSize.y),
                Mathf.FloorToInt(localPosition.z / cellSize.z)
            );

            // Add the triangle to the corresponding cluster
            if (!clusters.ContainsKey(cellIndex))
                clusters[cellIndex] = new List<int>();

            clusters[cellIndex].Add(triangles[i]);
            clusters[cellIndex].Add(triangles[i + 1]);
            clusters[cellIndex].Add(triangles[i + 2]);
        }

        // Generate fragments from clusters
        foreach (var cluster in clusters)
        {
            CreateFragment(cluster.Value, vertices, meshRenderer.material);
        }

        // Destroy the original object
        Destroy(gameObject);
    }

    private void CreateFragment(List<int> clusterTriangles, Vector3[] vertices, Material material)
    {
        // Create a new fragment object
        GameObject fragment = new GameObject("Fragment");
        fragment.transform.position = transform.position;
        fragment.transform.rotation = transform.rotation;

        Mesh fragmentMesh = new Mesh();

        // Extract vertices and triangles for the fragment
        Vector3[] fragmentVertices = new Vector3[clusterTriangles.Count];
        int[] fragmentIndices = new int[clusterTriangles.Count];

        for (int i = 0; i < clusterTriangles.Count; i++)
        {
            int vertexIndex = clusterTriangles[i];
            fragmentVertices[i] = vertices[vertexIndex];
            fragmentIndices[i] = i;
        }

        fragmentMesh.vertices = fragmentVertices;
        fragmentMesh.triangles = fragmentIndices;
        fragmentMesh.RecalculateNormals();

        // Assign the mesh to the fragment
        MeshFilter fragmentFilter = fragment.AddComponent<MeshFilter>();
        fragmentFilter.mesh = fragmentMesh;

        MeshRenderer fragmentRenderer = fragment.AddComponent<MeshRenderer>();
        fragmentRenderer.material = material;

        // Add physics components
        Rigidbody rb = fragment.AddComponent<Rigidbody>();
        MeshCollider collider = fragment.AddComponent<MeshCollider>();
        DespawnShrapnel despawn = fragment.AddComponent<DespawnShrapnel>();
        collider.sharedMesh = fragmentMesh;
        collider.convex = true;

        // Add explosion force
        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardModifier, ForceMode.Impulse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }
}
