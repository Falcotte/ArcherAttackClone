using UnityEngine;

namespace ArcherAttack.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public void DisableAnimator() => _animator.enabled = false;

        public void Attack() => _animator.SetTrigger("Attack");
    }
}