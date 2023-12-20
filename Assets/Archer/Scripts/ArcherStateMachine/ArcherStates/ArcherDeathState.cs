using ArcherAttack.Game;

namespace ArcherAttack.Archer
{
    public class ArcherDeathState : ArcherBaseState
    {
        public override void EnterState(ArcherStateMachine stateMachine)
        {
            stateMachine.Archer.AnimationController.Die();
            GameManager.Instance.LoseGame();
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {

        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {

        }

        public override string ToString()
        {
            return "Death";
        }
    }
}