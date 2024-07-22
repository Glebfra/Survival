using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float rotationSpeed = 1.0f;
        [SerializeField] private float lookSpeed = 1.0f;
        [SerializeField] private float cameraDistance = 2.0f;

        private GameObject _springArm;
        private GameObject _camera;

        private Vector2 _rotation = Vector2.zero;

        private void Start()
        {
            _rotation.y = transform.eulerAngles.y;

            _springArm = CreateSpringArm(gameObject);
            _camera = CreateCamera(_springArm, cameraDistance);
        }

        private void Update()
        {
            Rotate();
            Move();
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            _rotation += new Vector2(-mouseY, mouseX) * lookSpeed;
            _rotation.x = Mathf.Clamp(_rotation.x, -90.0f, 90.0f);
            _springArm.transform.rotation = Quaternion.Euler(_rotation);
        }

        private void Move()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            if (vertical == 0 && horizontal == 0) return;
            Vector3 forwardMovement = transform.forward * vertical;
            Vector3 horizontalMovement = transform.right * horizontal;
            Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);
            Quaternion rotation = Quaternion.Euler(0.0f, _springArm.transform.rotation.eulerAngles.y, 0.0f);

            transform.Translate(movement * (speed * Time.deltaTime), Space.World);
            transform.rotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * rotationSpeed);
        }
        
        private static GameObject CreateSpringArm(GameObject target)
        {
            GameObject springArmObject = new GameObject("Spring Arm");

            Transform springArmTransform = springArmObject.transform;
            springArmTransform.parent = target.transform;
            springArmTransform.localPosition = Vector3.zero;

            return springArmObject;
        }

        private static GameObject CreateCamera(GameObject target, float cameraDistance)
        {
            GameObject cameraObject = new GameObject("Camera");
            cameraObject.AddComponent<Camera>();

            Transform cameraTransform = cameraObject.transform;
            cameraTransform.parent = target.transform;
            cameraTransform.localPosition = -target.transform.forward * cameraDistance;
            cameraTransform.localRotation = target.transform.rotation;

            return cameraObject;
        }
    }
}