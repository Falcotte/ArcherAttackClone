using AngryKoala.Ragdoll;
using UnityEngine;

namespace ArcherAttack.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyStateMachine _stateMachine;
        public EnemyStateMachine StateMachine => _stateMachine;

        [SerializeField] private EnemyAnimationController _animationController;
        public EnemyAnimationController AnimationController => _animationController;

        [SerializeField] private EnemyHealthController _healthController;
        public EnemyHealthController HealthController => _healthController;

        [SerializeField] private EnemyShooterController _shooterController;
        public EnemyShooterController ShooterController => _shooterController;

        [SerializeField] private Ragdoll _ragdoll;
        public Ragdoll Ragdoll => _ragdoll;

        [SerializeField] private int _enemyIndex;
        public int EnemyIndex => _enemyIndex;

        private void Awake()
        {
            _ragdoll.DisableRagdoll();
        }
    }
}