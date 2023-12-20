using ArcherAttack.Archer;
using ArcherAttack.Game;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArcherAttack.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Image _crosshair;

        [SerializeField] private Color _crosshairMissColor;
        [SerializeField] private Color _crosshairHitColor;

        [SerializeField] private Image _arrowCountUI;
        [SerializeField] private TextMeshProUGUI _arrowCountText;

        private void OnEnable()
        {
            GameManager.OnGameStart += ShowArrowCountUI;
            GameManager.OnGameLose += HideArrowCountUI;
            GameManager.OnGameWin += HideArrowCountUI;

            ArcherShooterController.OnArrowCountUpdated += UpdateArrowCount;

            ArcherController.OnAimed += ShowCrosshair;
            ArcherShooterController.OnShoot += HideCrosshair;
            GameManager.OnGameLose += HideCrosshair;

            ArcherShooterController.OnHitDetected += SetCrosshairColor;
        }

        private void OnDisable()
        {
            GameManager.OnGameStart -= ShowArrowCountUI;
            GameManager.OnGameLose -= HideArrowCountUI;
            GameManager.OnGameWin -= HideArrowCountUI;

            ArcherShooterController.OnArrowCountUpdated -= UpdateArrowCount;

            ArcherController.OnAimed -= ShowCrosshair;
            ArcherShooterController.OnShoot -= HideCrosshair;
            GameManager.OnGameLose -= HideCrosshair;

            ArcherShooterController.OnHitDetected -= SetCrosshairColor;
        }

        private void Start()
        {
            _arrowCountUI.transform.localScale = Vector3.zero;
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

        private void ShowArrowCountUI()
        {
            _arrowCountUI.transform.DOScale(1f, .5f).SetEase(Ease.OutBack);
        }

        private void HideArrowCountUI()
        {
            _arrowCountUI.transform.DOScale(0f, .5f);
        }

        private void UpdateArrowCount(int arrowCount)
        {
            _arrowCountText.text = arrowCount.ToString();
        }
    }
}