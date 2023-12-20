using ArcherAttack.Archer;
using ArcherAttack.Game;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ArcherAttack.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Image _crosshair;

        [SerializeField] private Color _crosshairMissColor;
        [SerializeField] private Color _crosshairHitColor;

        private void OnEnable()
        {
            ArcherController.OnAimed += ShowCrosshair;
            ArcherShooterController.OnShoot += HideCrosshair;
            GameManager.OnGameLose += HideCrosshair;

            ArcherShooterController.OnHitDetected += SetCrosshairColor;
        }

        private void OnDisable()
        {
            ArcherController.OnAimed -= ShowCrosshair;
            ArcherShooterController.OnShoot -= HideCrosshair;
            GameManager.OnGameLose -= HideCrosshair;

            ArcherShooterController.OnHitDetected -= SetCrosshairColor;
        }

        private void ShowCrosshair()
        {
            _crosshair.transform.DOScale(1f, .25f);
        }

        private void HideCrosshair()
        {
            _crosshair.transform.DOScale(0f, .25f);
        }

        private void SetCrosshairColor(bool isHit)
        {
            if(isHit)
            {
                _crosshair.color = _crosshairHitColor;
            }
            else
            {
                _crosshair.color = _crosshairMissColor;
            }
        }
    }
}