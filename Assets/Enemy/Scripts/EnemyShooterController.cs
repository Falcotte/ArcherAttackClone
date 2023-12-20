using ArcherAttack.Archer;
using UnityEngine;

namespace ArcherAttack.Enemy
{
    public class EnemyShooterController : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemy;
        public EnemyController Enemy => _enemy;

        [SerializeField] private Transform _target;
        public Transform Target => _target;

        private void OnEnable()
        {
            ArcherShooterController.OnEnemyMissed += AttackPlayer;
        }

        private void OnDisable()
        {
            ArcherShooterController.OnEnemyMissed -= AttackPlayer;
        }

        private void AttackPlayer(int enemyIndex)
        {
            if(_enemy.EnemyIndex == enemyIndex)
            {
                _enemy.EnemyStateMachine.ChangeState(_enemy.EnemyStateMachine.AttackState);
            }
        }
    }
}