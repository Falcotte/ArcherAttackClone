using DG.Tweening;
using UnityEngine;

namespace ArcherAttack.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private GameObject _muzzleFlash;
        public GameObject MuzzleFlash => _muzzleFlash;

        public void DisableAnimator() => _animator.enabled = false;

        public void Attack() => _animator.SetTrigger("Attack");

        public void ShowMuzzleFlash()
        {
            Sequence muzzleFlashSequence = DOTween.Sequence();
            muzzleFlashSequence.AppendCallback(() => _muzzleFlash.SetActive(true));
            muzzleFlashSequence.AppendInterval(.02f);
            muzzleFlashSequence.AppendCallback(() => _muzzleFlash.SetActive(false));
        }
    }
}