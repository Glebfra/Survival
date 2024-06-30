using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float Speed = 1.0f;
        private const float RotationSpeed = 5.0f;
        private const float CameraSensitive = 500f;

        private float _yaw;
        private float _pitch;

        private GameObject _springArm;
        private GameObject _camera;

        private Transform _cameraTransform;
        
        private void Start()
        {
            _springArm = new GameObject("Spring Arm")
            {
                transform =
                {
                    parent = transform,
                    localPosition = new Vector3()
                }
            };

            _camera = new GameObject("Camera")
            {
                transform =
                {
                    parent = _springArm.transform,
                    localPosition = new Vector3()
                }
            };
            _camera.transform.localPosition -= transform.forward * 2;
            _camera.AddComponent<Camera>();
            _cameraTransform = _camera.transform;
        }

        private void Update()
        {
            _yaw += Input.GetAxis("Mouse X") * CameraSensitive * Time.deltaTime;
            _pitch -= Input.GetAxis("Mouse Y") * CameraSensitive * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, -90.0f, 90.0f);
            _springArm.transform.rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);

            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) return;
            Vector3 dir =
                (transform.forward * Input.GetAxis("Vertical") +
                 transform.right * Input.GetAxis("Horizontal")) * (Time.deltaTime * Speed);
            dir.y = 0;
            transform.Translate(dir);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, _yaw, 0), Time.deltaTime * RotationSpeed);
        }
    }
}