using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class TerrainGenerator : MonoBehaviour
    {
        public Vector3 terrainScale;

        private const int TerrainMeshesNumber = 16;

        private List<TerrainMesh> _terrainMeshes;

        private void Start()
        {
            _terrainMeshes = new List<TerrainMesh>();

            var terrainMeshes = (int)Mathf.Sqrt(TerrainMeshesNumber);
            for (var i = -terrainMeshes; i < terrainMeshes; i++)
            for (var j = -terrainMeshes; j < terrainMeshes; j++)
            {
                GameObject obj = new GameObject("TerrainMesh");
                obj.transform.parent = transform;
                obj.transform.localScale = terrainScale;
                obj.transform.position += new Vector3(i, 0, j) * terrainScale.x;
                TerrainMesh terrainMesh = obj.AddComponent<TerrainMesh>();
                _terrainMeshes.Add(terrainMesh);
            }
        }
    }
}