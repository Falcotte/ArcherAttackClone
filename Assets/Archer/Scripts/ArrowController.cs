using UnityEngine;
using UnityEngine.Events;

namespace ArcherAttack.Archer
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _maxAllowedFlightTimeWithoutCollision;

        private bool _isMoving;
        private float _flightTime;

        public static UnityAction OnArrowMissed;

        private void Update()
        {
            if(_isMoving)
            {
                _flightTime += Time.deltaTime;
                if(_flightTime >= _maxAllowedFlightTimeWithoutCollision)
                {
                    OnArrowMissed?.Invoke();
                }
            }
        }

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
            Debug.Log("Hit Environment");
            OnArrowMissed?.Invoke();

            _isMoving = false;
        }
    }
}