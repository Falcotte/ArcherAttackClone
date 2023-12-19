using UnityEngine;

namespace ArcherAttack.Archer
{
    public class ArcherStateMachine : MonoBehaviour
    {
        [SerializeField] private ArcherController _archer;
        public ArcherController Archer => _archer;

        private ArcherBaseState _currentState;

        private ArcherIdleState _idleState = new();
        public ArcherIdleState IdleState => _idleState;
        private ArcherMoveState _moveState = new();
        public ArcherMoveState MoveState => _moveState;
        private ArcherDrawState _drawState = new();
        public ArcherDrawState DrawState => _drawState;

        private ArcherAimState _aimState = new();
        public ArcherAimState AimState => _aimState;

        private ArcherShootState _shootState = new();
        public ArcherShootState ShootState => _shootState;

        private void Start()
        {
            _currentState = _idleState;
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void ChangeState(ArcherBaseState nextState)
        {
            _currentState.ExitState(this);

            Debug.Log($"Archer changing state to {nextState}");

            _currentState = nextState;
            nextState.EnterState(this);
        }
    }
}