using AngryKoala.Ragdoll;
using UnityEngine;

namespace ArcherAttack.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Ragdoll _ragdoll;
        public Ragdoll Ragdoll => _ragdoll;
        [SerializeField] private EnemyAnimationController _animationController;
        public EnemyAnimationController AnimationController => _animationController;

        private void Awake()
        {
            _ragdoll.DisableRagdoll();
        }
    }
}