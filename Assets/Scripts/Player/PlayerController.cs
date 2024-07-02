using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float Speed = 1.0f;
        private const float RotationSpeed = 2.0f;
        private const float CameraSensitive = 500f;

        private float _yaw;
        private float _pitch;

        private GameObject _springArm;
        private GameObject _camera;

        private void Start()
        {
            Transform gameObjectTransform = transform;
            _springArm = new GameObject("Spring Arm")
            {
                transform =
                {
                    parent = gameObjectTransform,
                    localPosition = new Vector3()
                }
            };

            _camera = new GameObject("Camera")
            {
                transform =
                {
                    parent = _springArm.transform,
                    rotation = gameObjectTransform.rotation,
                    localPosition = new Vector3() - gameObjectTransform.forward * 2
                }
            };
            _camera.AddComponent<Camera>();
        }
        
        private void Update()
        {
            _yaw += Input.GetAxis("Mouse X") * CameraSensitive * Time.deltaTime;
            _pitch -= Input.GetAxis("Mouse Y") * CameraSensitive * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, -90.0f, 90.0f);
            _springArm.transform.rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);

            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                Vector3 dir = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")).normalized;
                transform.position += dir * (Time.deltaTime * Speed);
                transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, _yaw, 0), Time.deltaTime * RotationSpeed);
            }
        }
    }
}