using DG.Tweening;

namespace ArcherAttack.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        public override void EnterState(EnemyStateMachine stateMachine)
        {
            stateMachine.transform.DOLookAt(stateMachine.Enemy.ShooterController.Target.position, .3f);

            stateMachine.Enemy.AnimationController.Attack();
        }

        public override void ExitState(EnemyStateMachine stateMachine)
        {

        }

        public override void UpdateState(EnemyStateMachine stateMachine)
        {

        }

        public override string ToString()
        {
            return "Attack";
        }
    }
}