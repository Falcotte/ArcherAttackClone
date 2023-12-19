using UnityEngine;
using UnityEngine.Events;

namespace ArcherAttack.Archer
{
    public class ArcherAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public static UnityAction OnDrawCompleted;

        public void SetMovement(bool isMoving) => _animator.SetBool("IsMoving", isMoving);

        public void CompleteDraw() => OnDrawCompleted?.Invoke();

        public void Aim() => _animator.SetTrigger("Aim");

        public void ResetAim() => _animator.ResetTrigger("Aim");

        public void Shoot() => _animator.SetTrigger("Shoot");

        public void ResetShoot() => _animator.ResetTrigger("Shoot");
    }
}