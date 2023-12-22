using UnityEngine;
using UnityEngine.Events;
using static AngryKoala.Ragdoll.RagdollComponent;

namespace ArcherAttack.Enemy
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemy;

        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        [SerializeField] private Outline _outline;

        public static UnityAction OnEnemyDamaged;
        public static UnityAction OnEnemyDeath;

        public static UnityAction<BodyParts> OnEnemyKilledByArrow;
        public static UnityAction OnEnemyKilledByExplosion;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage()
        {
            _currentHealth--;
            if(_currentHealth <= 0)
            {
                Die();
            }
            else
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.AttackState);
                OnEnemyDamaged?.Invoke();
            }
        }

        public void TakeDamage(BodyParts bodyPart)
        {
            _currentHealth--;
            if(_currentHealth <= 0)
            {
                OnEnemyKilledByArrow?.Invoke(bodyPart);
                Die();
            }
            else
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.AttackState);
                OnEnemyDamaged?.Invoke();
            }
        }

        public void Explode()
        {
            Die();

            OnEnemyKilledByExplosion?.Invoke();
        }

        private void Die()
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.DeathState);
            _outline.enabled = false;

            OnEnemyDeath?.Invoke();
        }
    }
}