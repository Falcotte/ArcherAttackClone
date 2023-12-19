using UnityEngine;

namespace ArcherAttack.Archer
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isMoving;

        private void FixedUpdate()
        {
            if(_isMoving)
            {
                MoveForward();
            }
        }

        public void StartMovement()
        {
            _isMoving = true;
        }

        private void MoveForward()
        {
            _rigidbody.MovePosition(_rigidbody.position + transform.forward * _moveSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            _isMoving = false;
        }
    }
}