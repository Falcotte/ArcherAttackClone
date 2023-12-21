using AngryKoala.Ragdoll;
using ArcherAttack.Game;
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
        public static UnityAction OnArrowHit;

        private void Update()
        {
            if(_isMoving)
            {
                _flightTime += Time.deltaTime;
                if(_flightTime >= _maxAllowedFlightTimeWithoutCollision)
                {
                    OnArrowMissed?.Invoke();
                    Destroy(gameObject);
                }
            }

            if(GameManager.Instance.CurrentState == GameState.GameLose)
            {
                gameObject.SetActive(false);
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
            if(!_isMoving)
                return;

            if(other.TryGetComponent(out RagdollComponent ragdollComponent))
            {
                ragdollComponent.Ragdoll.Enemy.HealthController.TakeDamage(ragdollComponent.BodyPart);

                transform.SetParent(ragdollComponent.transform);
                OnArrowHit?.Invoke();
            }
            else
            {
                Debug.Log("Hit Environment");
                OnArrowMissed?.Invoke();
            }
            _isMoving = false;
        }
    }
}