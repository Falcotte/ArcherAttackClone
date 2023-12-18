using UnityEngine;

namespace ArcherAttack.Archer
{
    public class ArcherIdleState : ArcherBaseState
    {
        public override void EnterState(ArcherStateMachine stateMachine)
        {

        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {

        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.Archer.MoveToNextWaypoint();
                stateMachine.ChangeState(stateMachine.MoveState);
            }
        }
    }
}