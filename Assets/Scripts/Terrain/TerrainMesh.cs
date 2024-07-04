using UnityEngine;

namespace Terrain
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))]
    public class TerrainMesh : MonoBehaviour
    {
        private const int MaxIterations = 3;
        
        private void Start()
        {
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            Mesh mesh = GenerateMesh();
            mesh.RecalculateNormals();
            meshFilter.sharedMesh = mesh;

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            Material material = new Material(Shader.Find("Unlit/Color"));
            meshRenderer.material = material;

            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
        }

        private Mesh GenerateMesh()
        {
            Vector3[,] points = DiamondSquare.GeneratePoints(MaxIterations);
            Vector3[] vertices = new Vector3[points.Length];
            int[] triangles = new int[6 * points.Length];

            int length = (int)Mathf.Sqrt(points.Length);
            
            int triangleIndex = 0;
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    vertices[index] = points[i, j];
                    
                    if (i != length - 1 && j != length - 1)
                    {
                        triangles[triangleIndex] = index;
                        triangles[triangleIndex + 1] = index + length + 1;
                        triangles[triangleIndex + 2] = index + length;

                        triangles[triangleIndex + 3] = index;
                        triangles[triangleIndex + 4] = index + 1;
                        triangles[triangleIndex + 5] = index + length + 1;
                        
                        triangleIndex += 6;
                    }
                    index++;
                }
            }

            Mesh mesh = new Mesh
            {
                vertices = vertices,
                triangles = triangles
            };
            return mesh;
        }
    }
}