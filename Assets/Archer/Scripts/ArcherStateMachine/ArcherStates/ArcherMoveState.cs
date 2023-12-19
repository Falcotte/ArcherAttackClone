using UnityEngine;

namespace ArcherAttack.Archer
{
    public class ArcherMoveState : ArcherBaseState
    {
        private const float ReachTargetThreshold = .02f;

        public override void EnterState(ArcherStateMachine stateMachine)
        {
            stateMachine.Archer.MovementController.MoveToNextWaypoint();
            stateMachine.Archer.AnimationController.SetMovement(true);
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {

        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            stateMachine.Archer.MovementController.UpdateWaypointProgression();

            if(Vector3.SqrMagnitude(stateMachine.Archer.transform.position - stateMachine.Archer.MovementController.CurrentDestination) <= ReachTargetThreshold)
            {
                stateMachine.Archer.MovementController.ReachWaypoint();
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }

        public override string ToString()
        {
            return "Moving";
        }
    }
}