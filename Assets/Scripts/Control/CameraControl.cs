using UnityEngine;

namespace Control
{
    public class CameraControl : MonoBehaviour
    {
        public GameObject SpringArm => _springArm;
        public GameObject Camera => _camera;
        
        [SerializeField] private float _lookSpeed = 1.0f;
        [SerializeField] private float _cameraDistance = 2.0f;
        
        private GameObject _springArm;
        private GameObject _camera;
        
        private Vector2 _rotation = Vector2.zero;
        
        private void Start()
        {
            _rotation.y = transform.eulerAngles.y;

            _springArm = CreateSpringArm(gameObject);
            _camera = CreateCamera(_springArm);
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            _rotation += new Vector2(-mouseY, mouseX) * _lookSpeed;
            _rotation.x = Mathf.Clamp(_rotation.x, -90.0f, 90.0f);
            _springArm.transform.rotation = Quaternion.Euler(_rotation);
        }

        private GameObject CreateSpringArm(GameObject target)
        {
            GameObject springArmObject = new GameObject("Spring Arm");

            Transform springArmTransform = springArmObject.transform;
            springArmTransform.parent = target.transform;
            springArmTransform.localPosition = Vector3.zero;

            return springArmObject;
        }
        
        private GameObject CreateCamera(GameObject target)
        {
            GameObject cameraObject = new GameObject("Camera");
            cameraObject.AddComponent<Camera>();

            Transform cameraTransform = cameraObject.transform;
            cameraTransform.parent = target.transform;
            cameraTransform.localPosition = -target.transform.forward * _cameraDistance;
            cameraTransform.localRotation = target.transform.rotation;

            return cameraObject;
        }
    }
}