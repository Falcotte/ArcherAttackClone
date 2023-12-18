using ArcherAttack.Inputs;

namespace ArcherAttack.Archer
{
    public class ArcherAimState : ArcherBaseState
    {
        private bool _isShootingTriggered;

        public override void EnterState(ArcherStateMachine stateMachine)
        {
            InputController.OnTouchDown += TriggerShooting;

            stateMachine.Archer.AnimationController.Aim();
            ArcherController.OnAimed?.Invoke();
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {
            InputController.OnTouchDown -= TriggerShooting;
        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            if(_isShootingTriggered)
            {
                stateMachine.Archer.AnimationController.Shoot();
                stateMachine.ChangeState(stateMachine.MoveState);

                _isShootingTriggered = false;
            }
        }

        private void TriggerShooting()
        {
            _isShootingTriggered = true;
        }
    }
}