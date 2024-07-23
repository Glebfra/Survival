using UnityEngine;

namespace Control
{
    [RequireComponent(typeof(CameraControl))]
    public class RotationControl : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 1.0f;
        
        private CameraControl _cameraControl;
        
        private void Start()
        {
            _cameraControl = gameObject.GetComponent<CameraControl>();
        }

        private void Update()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            if (vertical == 0 && horizontal == 0) return;
            Transform springArmTransform = _cameraControl.SpringArm.transform;
            Quaternion rotation = Quaternion.Euler(0.0f, springArmTransform.rotation.eulerAngles.y, 0.0f);
            transform.rotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
}