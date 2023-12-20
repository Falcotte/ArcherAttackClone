using ArcherAttack.Game;
using ArcherAttack.Inputs;

namespace ArcherAttack.Archer
{
    public class ArcherIdleState : ArcherBaseState
    {
        private bool _isActionTriggered;

        public override void EnterState(ArcherStateMachine stateMachine)
        {
            InputController.OnTouchDown += TriggerAction;

            stateMachine.Archer.AnimationController.SetMovement(false);
        }

        public override void ExitState(ArcherStateMachine stateMachine)
        {
            InputController.OnTouchDown -= TriggerAction;
        }

        public override void UpdateState(ArcherStateMachine stateMachine)
        {
            if(_isActionTriggered)
            {
                if(stateMachine.Archer.MovementController.CurrentWaypointIndex == 0)
                {
                    GameManager.Instance.StartGame();
                    stateMachine.ChangeState(stateMachine.MoveState);
                }
                else if(GameManager.Instance.CurrentState != GameState.GameLose)
                {
                    stateMachine.ChangeState(stateMachine.DrawState);
                }

                _isActionTriggered = false;
            }
        }

        private void TriggerAction()
        {
            _isActionTriggered = true;
        }

        public override string ToString()
        {
            return "Idle";
        }
    }
}