using ArcherAttack.Game;

namespace ArcherAttack.Archer
{
    public class ArcherDeathState : ArcherBaseState
    {
        public override void EnterState(ArcherStateMachine stateMachine)
        {
            stateMachine.Archer.AnimationController.Die();
            GameManager.Instance.ChangeState(GameState.GameLose);
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {

        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {

        }
    }
}