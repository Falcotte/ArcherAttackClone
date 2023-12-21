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

        [SerializeField] private CanvasGroup _mainMenuUI;
        [SerializeField] private CanvasGroup _gameplayUI;
        [SerializeField] private CanvasGroup _winUI;
        [SerializeField] private CanvasGroup _loseUI;

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _retryButton;

        private void OnEnable()
        {
            GameManager.OnGameStart += ShowArrowCountUI;
            GameManager.OnGameLose += HideArrowCountUI;
            GameManager.OnGameWin += HideArrowCountUI;

            GameManager.OnGameStart += ShowGameplayUI;
            GameManager.OnGameWin += ShowWinUI;
            GameManager.OnGameLose += ShowLoseUI;

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

            GameManager.OnGameStart -= ShowGameplayUI;
            GameManager.OnGameWin -= ShowWinUI;
            GameManager.OnGameLose -= ShowLoseUI;

            ArcherShooterController.OnArrowCountUpdated -= UpdateArrowCount;

            ArcherController.OnAimed -= ShowCrosshair;
            ArcherShooterController.OnShoot -= HideCrosshair;
            GameManager.OnGameLose -= HideCrosshair;

            ArcherShooterController.OnHitDetected -= SetCrosshairColor;
        }

        private void Start()
        {
            _arrowCountUI.transform.localScale = Vector3.zero;

            _mainMenuUI.alpha = 1f;
            _mainMenuUI.gameObject.SetActive(true);
            _gameplayUI.alpha = 0f;
            _gameplayUI.gameObject.SetActive(false);
            _winUI.alpha = 0f;
            _winUI.gameObject.SetActive(false);
            _loseUI.alpha = 0f;
            _loseUI.gameObject.SetActive(false);
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

        private void ShowMainMenuUI()
        {
            _startGameButton.enabled = false;

            _mainMenuUI.gameObject.SetActive(true);
            _mainMenuUI.DOFade(1f, .25f).OnComplete(() =>
            {
                _startGameButton.enabled = true;
            });
        }

        private void HideMainMenuUI()
        {
            _mainMenuUI.DOFade(0f, .25f).OnComplete(() =>
            {
                _mainMenuUI.gameObject.SetActive(false);
            });
        }

        public void StartGame()
        {
            HideMainMenuUI();

            _startGameButton.enabled = false;

            GameManager.Instance.StartGame();
        }

        private void ShowGameplayUI()
        {
            _gameplayUI.gameObject.SetActive(true);
            _gameplayUI.DOFade(1f, .25f);
        }

        private void HideGameplayUI()
        {
            _gameplayUI.DOFade(0f, .25f).OnComplete(() =>
            {
                _gameplayUI.gameObject.SetActive(false);
            });
        }

        private void ShowWinUI()
        {
            HideGameplayUI();

            Sequence showWinUISequence = DOTween.Sequence();
            showWinUISequence.AppendInterval(1.5f);
            showWinUISequence.AppendCallback(() =>
            {
                _continueButton.enabled = false;

                _winUI.gameObject.SetActive(true);
                _winUI.DOFade(1f, .25f).OnComplete(() =>
                {
                    _continueButton.enabled = true;
                });
            });
        }

        private void HideWinUI()
        {
            _winUI.DOFade(0f, .25f).OnComplete(() =>
            {
                _winUI.gameObject.SetActive(false);
            });
        }

        private void ShowLoseUI()
        {
            HideGameplayUI();

            Sequence showLoseUISequence = DOTween.Sequence();
            showLoseUISequence.AppendInterval(1.5f);
            showLoseUISequence.AppendCallback(() =>
            {
                _retryButton.enabled = false;

                _loseUI.gameObject.SetActive(true);
                _loseUI.DOFade(1f, .25f).OnComplete(() =>
                {
                    _retryButton.enabled = true;
                });
            });
        }

        private void HideLoseUI()
        {
            _loseUI.DOFade(0f, .25f).OnComplete(() =>
            {
                _loseUI.gameObject.SetActive(false);
            });
        }

        public void Continue()
        {
            _continueButton.enabled = false;

            HideWinUI();
        }

        public void Retry()
        {
            _retryButton.enabled = false;

            HideLoseUI();
        }
    }
}