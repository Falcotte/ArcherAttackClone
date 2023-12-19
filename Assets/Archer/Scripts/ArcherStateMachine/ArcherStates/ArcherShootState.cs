namespace ArcherAttack.Archer
{
    public class ArcherShootState : ArcherBaseState
    {
        public override void EnterState(ArcherStateMachine stateMachine)
        {
            stateMachine.Archer.ShooterController.ShootArrow();
            stateMachine.Archer.AnimationController.Shoot();

            stateMachine.ChangeState(stateMachine.MoveState);
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {

        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {

        }
    }
}