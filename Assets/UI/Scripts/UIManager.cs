using ArcherAttack.Archer;
using ArcherAttack.Enemy;
using ArcherAttack.Game;
using ArcherAttack.Game.Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AngryKoala.Ragdoll.RagdollComponent;

namespace ArcherAttack.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private Camera _uICamera;
        public Camera UICamera => _uICamera;

        [SerializeField] private Image _crosshair;

        [SerializeField] private Color _crosshairMissColor;
        [SerializeField] private Color _crosshairHitColor;

        [SerializeField] private Image _arrowCountUI;
        [SerializeField] private TextMeshProUGUI _arrowCountText;

        [SerializeField] private CanvasGroup _mainMenuUI;
        [SerializeField] private CanvasGroup _gameplayUI;
        [SerializeField] private CanvasGroup _winUI;
        [SerializeField] private CanvasGroup _loseUI;

        [SerializeField] private Image _gettingAttackedIndicator;

        [SerializeField] private Image _killTypeImageBackground;
        [SerializeField] private Image _killTypeImage;
        [SerializeField] private TextMeshProUGUI _killTypeText;

        [SerializeField] private Sprite _killTypeHeadshot;
        [SerializeField] private Sprite _killTypeBody;
        [SerializeField] private Sprite _killTypeArms;
        [SerializeField] private Sprite _killTypeLegs;

        [SerializeField] private TextMeshProUGUI _defeatMessage;

        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private TextMeshProUGUI _currencyRewardText;

        [SerializeField] private TextMeshProUGUI _levelText;

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _retryButton;

        private Color _gettingAttackedDefaultColor;
        private int _arrowCount;

        private void OnEnable()
        {
            GameManager.OnGameStart += ShowArrowCountUI;
            GameManager.OnGameLose += HideArrowCountUI;
            GameManager.OnGameWin += HideArrowCountUI;

            GameManager.OnGameStart += ShowGameplayUI;
            GameManager.OnGameWin += ShowWinUI;
            GameManager.OnGameLose += ShowLoseUI;

            ArcherShooterController.OnArrowCountUpdated += UpdateArrowCount;
            EnemyHealthController.OnEnemyKilledByArrow += ShowKillType;

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
            EnemyHealthController.OnEnemyKilledByArrow -= ShowKillType;

            ArcherController.OnAimed -= ShowCrosshair;
            ArcherShooterController.OnShoot -= HideCrosshair;
            GameManager.OnGameLose -= HideCrosshair;

            ArcherShooterController.OnHitDetected -= SetCrosshairColor;
        }

        private void Start()
        {
            _arrowCountUI.transform.localScale = Vector3.zero;

            _levelText.text = $"LEVEL {DataManager.PlayerData.Level}";
            _currencyText.text = $"${DataManager.PlayerData.Currency}";

            _killTypeImageBackground.gameObject.SetActive(false);
            _killTypeImageBackground.transform.localScale = Vector3.zero;

            _gettingAttackedDefaultColor = _gettingAttackedIndicator.color;

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
            _arrowCount = arrowCount;
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

        private void ShowKillType(BodyParts bodyPart)
        {
            _killTypeImageBackground.gameObject.SetActive(true);

            switch(bodyPart)
            {
                case BodyParts.Head:
                    _killTypeText.text = "HEADSHOT";
                    _killTypeImage.sprite = _killTypeHeadshot;
                    break;
                case BodyParts.CenterMass:
                    _killTypeText.text = "Body";
                    _killTypeImage.sprite = _killTypeBody;
                    break;
                case BodyParts.Arm:
                    _killTypeText.text = "Arm";
                    _killTypeImage.sprite = _killTypeArms;
                    break;
                case BodyParts.Leg:
                    _killTypeText.text = "Legs";
                    _killTypeImage.sprite = _killTypeLegs;
                    break;
            }

            Sequence showKillTypeSequence = DOTween.Sequence();
            showKillTypeSequence.Append(_killTypeImageBackground.transform.DOScale(1f, .25f).SetEase(Ease.OutBack));
            showKillTypeSequence.AppendInterval(1f);
            showKillTypeSequence.Append(_killTypeImageBackground.transform.DOScale(0f, .25f)).OnComplete(() =>
            {
                _killTypeImageBackground.gameObject.SetActive(false);
            });
        }

        public void ShowGettingAttackedIndicator()
        {
            if(_gettingAttackedIndicator.gameObject.activeInHierarchy)
                return;

            _gettingAttackedIndicator.gameObject.SetActive(true);
            _gettingAttackedIndicator.color = new Color(_gettingAttackedDefaultColor.r, _gettingAttackedDefaultColor.g, _gettingAttackedDefaultColor.b, 0f);
            _gettingAttackedIndicator.DOFade(.05f, .5f).SetLoops(-1, LoopType.Yoyo).SetId("GettingAttacked");
        }

        public void HideGettingAttackedIndicator()
        {
            DOTween.Kill("GettingAttacked");
            _gettingAttackedIndicator.gameObject.SetActive(false);
        }

        private void ShowWinUI()
        {
            HideGameplayUI();

            Sequence showWinUISequence = DOTween.Sequence();
            showWinUISequence.AppendInterval(1.5f);
            showWinUISequence.AppendCallback(() =>
            {
                _continueButton.enabled = false;

                _currencyRewardText.text = $"+{(DataManager.PlayerData.Level + 1) * 100}";
                _currencyText.text = $"${DataManager.PlayerData.Currency}";

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
                _levelText.text = $"LEVEL {DataManager.PlayerData.Level}";
            });
        }

        private void ShowLoseUI()
        {
            HideGameplayUI();

            if(_arrowCount <= 0)
            {
                _defeatMessage.text = "Out of Arrows";
            }
            else
            {
                _defeatMessage.text = "Killed by Enemy";
            }
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

            LoadingManager.Instance.LoadNextLevel();

            HideWinUI();
            ShowGameplayUI();
        }

        public void Retry()
        {
            _retryButton.enabled = false;

            LoadingManager.Instance.ReloadLevel();

            HideLoseUI();
            ShowGameplayUI();
        }
    }
}