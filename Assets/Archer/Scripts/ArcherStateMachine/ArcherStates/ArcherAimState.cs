using ArcherAttack.Inputs;

namespace ArcherAttack.Archer
{
    public class ArcherAimState : ArcherBaseState
    {
        private bool _isShootingTriggered;

        public override void EnterState(ArcherStateMachine stateMachine)
        {
            InputController.OnTouchUp += TriggerShooting;

            stateMachine.Archer.AnimationController.Aim();
            ArcherController.OnAimed?.Invoke();

            stateMachine.Archer.ShooterController.SpawnArrow();
            stateMachine.Archer.ShooterController.AnimateBowAndArrow();
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {
            InputController.OnTouchUp -= TriggerShooting;

            stateMachine.Archer.AnimationController.ResetAim();
            //stateMachine.Archer.ShooterController.ResetBowAndArrow();
        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            if(_isShootingTriggered)
            {
                stateMachine.ChangeState(stateMachine.ShootState);
                _isShootingTriggered = false;
            }

            stateMachine.Archer.ShooterController.CalculateArrowPath();
        }

        private void TriggerShooting()
        {
            _isShootingTriggered = true;
        }

        public override string ToString()
        {
            return "Aiming";
        }
    }
}