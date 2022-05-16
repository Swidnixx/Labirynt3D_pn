using UnityEngine;

public class Wobble : MonoBehaviour
{
    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        for (var i = 0; i < vertices.Length; i++)
        {
            normals[i] += normals[i] * Mathf.Sin(Time.time);
        }

        mesh.vertices = vertices;
    }
}