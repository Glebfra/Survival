using UnityEngine;

namespace Movement
{
    public class SurfaceSlider : MonoBehaviour
    {
        private Vector3 _normal;

        public Vector3 Project(Vector3 direction)
        {
            return direction - Vector3.Dot(direction, _normal) * _normal;
        }

        private void OnCollisionEnter(Collision other)
        {
            _normal = other.contacts[0].normal;
        }
    }
}