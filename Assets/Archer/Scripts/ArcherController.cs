using ArcherAttack.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace ArcherAttack.Archer
{
    public class ArcherController : MonoBehaviour
    {
        [SerializeField] private ArcherStateMachine _stateMachine;
        public ArcherStateMachine StateMachine => _stateMachine;

        [SerializeField] private ArcherMovementController _movementController;
        public ArcherMovementController MovementController => _movementController;

        [SerializeField] private ArcherAnimationController _animationController;
        public ArcherAnimationController AnimationController => _animationController;

        [SerializeField] private ArcherShooterController _shooterController;
        public ArcherShooterController ShooterController => _shooterController;

        public static UnityAction OnAimed;

        private void OnEnable()
        {
            EnemyAttackState.OnPlayerKilled += KillPlayer;
        }

        private void OnDisable()
        {
            EnemyAttackState.OnPlayerKilled -= KillPlayer;
        }

        private void KillPlayer()
        {
            _stateMachine.ChangeState(_stateMachine.DeathState);
        }
    }
}