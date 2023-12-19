namespace ArcherAttack.Archer
{
    public class ArcherShootState : ArcherBaseState
    {
        private bool _missed;

        public override void EnterState(ArcherStateMachine stateMachine)
        {
            stateMachine.Archer.ShooterController.ShootArrow();
            stateMachine.Archer.AnimationController.Shoot();

            ArrowController.OnArrowMissed += TriggerMiss;

        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {
            ArrowController.OnArrowMissed -= TriggerMiss;
        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            if(_missed)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                _missed = false;
            }
        }

        private void TriggerMiss()
        {
            _missed = true;
        }
    }
}