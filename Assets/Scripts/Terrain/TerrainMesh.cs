using UnityEngine;

namespace Terrain
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))]
    public class TerrainMesh : MonoBehaviour
    {
        private const int Resolution = 16;

        private void Start()
        {
            Vector3[] vertices = new Vector3[Resolution * Resolution];
            int[] triangles = new int[6 * Resolution * Resolution];

            int index = 0;
            int triangleIndex = 0;
            for (int x = 0; x < Resolution; x++)
            {
                for (int y = 0; y < Resolution; y++)
                {
                    Vector2 percent = new Vector2(x, y) / (Resolution - 1);
                    percent = (percent - new Vector2(0.5f, 0.5f)) * 2; 
                    Vector3 pointOnPlane = transform.forward * percent.y + transform.right * percent.x;
                    vertices[index] = pointOnPlane;

                    if (x != Resolution - 1 && y != Resolution - 1)
                    {
                        triangles[triangleIndex] = index;
                        triangles[triangleIndex + 1] = index + Resolution + 1;
                        triangles[triangleIndex + 2] = index + Resolution;

                        triangles[triangleIndex + 3] = index;
                        triangles[triangleIndex + 4] = index + 1;
                        triangles[triangleIndex + 5] = index + Resolution + 1;
                        
                        triangleIndex += 6;
                    }
                    
                    index++;
                }
            }

            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            Mesh mesh = meshFilter.mesh;
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            Material material = new Material(Shader.Find("Unlit/Color"));
            meshRenderer.material = material;

            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
        }
    }
}