using UnityEngine;

namespace ArcherAttack.Archer
{
    public class ArcherMoveState : ArcherBaseState
    {
        private const float ReachTargetThreshold = .02f;

        public override void EnterState(ArcherStateMachine stateMachine)
        {

        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {

        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            if(Vector3.SqrMagnitude(stateMachine.Archer.transform.position - stateMachine.Archer.CurrentDestination) <= ReachTargetThreshold)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}