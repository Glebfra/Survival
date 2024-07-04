using UnityEngine;

namespace Terrain
{
    public static class DiamondSquare
    {
        public static Vector3[,] GeneratePoints(int maxIterations)
        {
            int length = (int)(Mathf.Pow(2, maxIterations) + 1);
            Vector3[,] squarePoints = new Vector3[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    squarePoints[i, j] = Vector3.Lerp(
                        Vector3.Lerp(Vector3.zero, Vector3.forward, j / ((float)length - 1)), 
                        Vector3.Lerp(Vector3.right, Vector3.forward + Vector3.right, j / ((float)length - 1)),
                        i / ((float)length - 1)
                    );
                }
            }
            
            return squarePoints;
        }

        public static float[,] GenerateHeights(Vector3[,] points)
        {
            int length = (int)Mathf.Sqrt(points.Length);
            float[,] heights = new float[length, length];
            
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    
                }
            }
            
            return heights;
        }
    }
}