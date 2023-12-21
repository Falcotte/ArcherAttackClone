using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

        private void Die()
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.DeathState);
            _outline.enabled = false;

            OnEnemyDeath?.Invoke();
        }
    }
}