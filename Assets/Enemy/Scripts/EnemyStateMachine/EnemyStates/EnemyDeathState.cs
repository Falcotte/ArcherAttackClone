namespace ArcherAttack.Enemy
{
    public class EnemyDeathState : EnemyBaseState
    {
        public override void EnterState(EnemyStateMachine stateMachine)
        {
            stateMachine.Enemy.Ragdoll.EnableRagdoll();
            stateMachine.Enemy.AnimationController.DisableAnimator();
        }

        public override void ExitState(EnemyStateMachine stateMachine)
        {

        }

        public override void UpdateState(EnemyStateMachine stateMachine)
        {

        }

        public override string ToString()
        {
            return "Death";
        }
    }
}