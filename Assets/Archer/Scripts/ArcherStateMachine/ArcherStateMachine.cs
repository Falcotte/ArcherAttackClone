using UnityEngine;

namespace ArcherAttack.Archer
{
    public class ArcherStateMachine : MonoBehaviour
    {
        [SerializeField] private ArcherController _archer;
        public ArcherController Archer => _archer;

        private ArcherBaseState _currentState;

        private ArcherIdleState _idleState = new ArcherIdleState();
        public ArcherIdleState IdleState => _idleState;
        private ArcherMoveState _moveState = new ArcherMoveState();
        public ArcherMoveState MoveState => _moveState;


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

            Debug.Log($"Changing state to {nextState.ToString()}");

            _currentState = nextState;
            nextState.EnterState(this);
        }
    }
}