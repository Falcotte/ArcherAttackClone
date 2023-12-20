using ArcherAttack.Game;
using DG.Tweening;
using UnityEngine.Events;

namespace ArcherAttack.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        public static UnityAction OnPlayerKilled;

        public override void EnterState(EnemyStateMachine stateMachine)
        {
            stateMachine.transform.DOLookAt(stateMachine.Enemy.ShooterController.Target.position, .3f);

            Sequence killPlayerSequence = DOTween.Sequence().SetId("KillPlayer");
            killPlayerSequence.AppendInterval(stateMachine.Enemy.ShooterController.SecondsToKillPlayer);
            killPlayerSequence.AppendCallback(() =>
            {
                if(GameManager.Instance.CurrentState != GameState.GameLose)
                {
                    OnPlayerKilled?.Invoke();
                }
            });

            stateMachine.Enemy.AnimationController.Attack();
        }

        public override void ExitState(EnemyStateMachine stateMachine)
        {
            DOTween.Kill("KillPlayer");
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