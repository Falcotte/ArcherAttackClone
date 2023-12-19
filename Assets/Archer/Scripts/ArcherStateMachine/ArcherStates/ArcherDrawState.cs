namespace ArcherAttack.Archer
{
    public class ArcherDrawState : ArcherBaseState
    {
        private bool _isAimingTriggered;

        public override void EnterState(ArcherStateMachine stateMachine)
        {
            ArcherAnimationController.OnDrawCompleted += TriggerAiming;

            stateMachine.Archer.AnimationController.Aim();
            ArcherController.OnAimed?.Invoke();
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {
            ArcherAnimationController.OnDrawCompleted -= TriggerAiming;
        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            if(_isAimingTriggered)
            {
                stateMachine.ChangeState(stateMachine.AimState);

                _isAimingTriggered = false;
            }
        }

        private void TriggerAiming()
        {
            _isAimingTriggered = true;
        }

        public override string ToString()
        {
            return "Draw";
        }
    }
}