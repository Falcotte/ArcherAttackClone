using ArcherAttack.Inputs;

namespace ArcherAttack.Archer
{
    public class ArcherIdleState : ArcherBaseState
    {
        private bool _isMovementTriggered;

        public override void EnterState(ArcherStateMachine stateMachine)
        {
            InputController.OnTouchDown += TriggerMovement;

            stateMachine.Archer.AnimationController.SetMovement(false);
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {
            InputController.OnTouchDown -= TriggerMovement;
        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            if(_isMovementTriggered)
            {
                stateMachine.Archer.MovementController.MoveToNextWaypoint();
                stateMachine.ChangeState(stateMachine.MoveState);

                _isMovementTriggered = false;
            }
        }

        private void TriggerMovement()
        {
            _isMovementTriggered = true;
        }
    }
}