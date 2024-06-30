using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerController), typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private void Start()
        {
            gameObject.GetComponent<PlayerController>();
            gameObject.GetComponent<Rigidbody>();
        }
    }
}