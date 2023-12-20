using UnityEngine;

namespace ArcherAttack.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemy;
        public EnemyController Enemy => _enemy;

        private EnemyBaseState _currentState;

        private EnemyIdleState _idleState = new();
        public EnemyIdleState IdleState => _idleState;
        private EnemyAttackState _attackState = new();
        public EnemyAttackState AttackState => _attackState;

        private EnemyDeathState _deathState = new();
        public EnemyDeathState DeathState => _deathState;

        private void Start()
        {
            _currentState = _idleState;
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void ChangeState(EnemyBaseState nextState)
        {
            if(nextState == _currentState)
            {
                return;
            }

            _currentState.ExitState(this);

            Debug.Log($"Enemy changing state to {nextState}");

            _currentState = nextState;
            nextState.EnterState(this);
        }
    }
}