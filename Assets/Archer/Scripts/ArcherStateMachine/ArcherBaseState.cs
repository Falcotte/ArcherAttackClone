namespace ArcherAttack.Archer
{
    public abstract class ArcherBaseState
    {
        public abstract void EnterState(ArcherStateMachine stateMachine);

        public abstract void UpdateState(ArcherStateMachine stateMachine);

        public abstract void ExitState(ArcherStateMachine stateMachine);
    }
}