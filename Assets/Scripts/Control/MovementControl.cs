using Movement;
using UnityEngine;

namespace Control
{
    [RequireComponent(typeof(Rigidbody), typeof(SurfaceSlider))]
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.0f;

        private Rigidbody _rigidbody;
        private SurfaceSlider _surfaceSlider;
        
        private void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _surfaceSlider = gameObject.GetComponent<SurfaceSlider>();
        }

        private void Update()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            if (vertical == 0 && horizontal == 0) return;
            Vector3 movement = _surfaceSlider.Project(transform.forward * vertical + transform.right * horizontal);
            _rigidbody.MovePosition(_rigidbody.position + movement * (_speed * Time.deltaTime));
        }
    }
}