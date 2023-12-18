using UnityEngine;

namespace ArcherAttack.Archer
{
    public class ArcherAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SetMovement(bool isMoving)
        {
            _animator.SetBool("IsMoving", isMoving);
        }

        public void Aim()
        {
            _animator.SetTrigger("Aim");
        }

        public void Shoot()
        {
            _animator.SetTrigger("Shoot");
        }
    }
}