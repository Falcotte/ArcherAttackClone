using ArcherAttack.Archer;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ArcherAttack.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Image _crosshair;

        private void OnEnable()
        {
            ArcherController.OnAimed += ShowCrosshair;
            ArcherShooterController.OnShoot += HideCrosshair;
        }

        private void OnDisable()
        {
            ArcherController.OnAimed -= ShowCrosshair;
            ArcherShooterController.OnShoot -= HideCrosshair;
        }

        private void ShowCrosshair()
        {
            _crosshair.transform.DOScale(1f, .25f);
        }

        private void HideCrosshair()
        {
            _crosshair.transform.DOScale(0f, .25f);
        }
    }
}