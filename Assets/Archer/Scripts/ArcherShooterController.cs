using ArcherAttack.Enemy;
using ArcherAttack.Game;
using ArcherAttack.Inputs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace ArcherAttack.Archer
{
    public class ArcherShooterController : MonoBehaviour
    {
        [SerializeField] private ArcherController _archer;

        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _spineBone;

        [SerializeField] private ArrowController _arrowPrefab;

        [SerializeField] private Transform _bowStringBone;

        [SerializeField] private Pose _arrowFinalPose;
        [SerializeField] private Pose _bowStringBoneFinalPose;

        [SerializeField] private float _inputSensitivity;
        [SerializeField] private float _inputHorizontalBound;
        [SerializeField] private float _inputVerticalBound;

        [SerializeField] private int _arrowCount;

        private Quaternion _spineBoneInitialRotation;

        private ArrowController _arrow;
        private Pose _bowStringBoneInitialPose;

        private Vector2 _currentInputVector;

        private Camera _mainCamera;
        private int _enemyLayer;

        public static UnityAction<bool> OnHitDetected;

        public static UnityAction OnShoot;
        public static UnityAction<int> OnArrowCountUpdated;
        public static UnityAction<int> OnEnemyMissed;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _enemyLayer = 1 << LayerMask.NameToLayer("EnemyRagdoll");

            _spineBoneInitialRotation = _spineBone.localRotation;

            _bowStringBone.GetLocalPositionAndRotation(out Vector3 bowStringBonePosition, out Quaternion bowStringBoneRotation);
            _bowStringBoneInitialPose = new Pose(bowStringBonePosition, bowStringBoneRotation);
        }

        private void OnEnable()
        {
            InputController.OnDragDelta += AdjustAimRotation;
            ArrowController.OnArrowMissed += MissEnemy;

            EnemyHealthController.OnEnemyDamaged += HitEnemy;
            EnemyHealthController.OnEnemyDeath += KillEnemy;
        }

        private void OnDisable()
        {
            InputController.OnDragDelta -= AdjustAimRotation;
            ArrowController.OnArrowMissed -= MissEnemy;

            EnemyHealthController.OnEnemyDamaged -= HitEnemy;
            EnemyHealthController.OnEnemyDeath -= KillEnemy;
        }

        private void Start()
        {
            OnArrowCountUpdated?.Invoke(_arrowCount);
        }

        private void LateUpdate()
        {
            _spineBone.Rotate(new Vector3(0f, _currentInputVector.x, -_currentInputVector.y), Space.Self);
        }

        public void SpawnArrow()
        {
            _arrow = Instantiate(_arrowPrefab);
        }

        public void AnimateBowAndArrow()
        {
            _arrow.transform.SetParent(_shootPoint);

            _arrow.transform.localPosition = _arrowFinalPose.position;
            _arrow.transform.localRotation = _arrowFinalPose.rotation;

            _bowStringBone.localPosition = _bowStringBoneFinalPose.position;
            _bowStringBone.localRotation = _bowStringBoneFinalPose.rotation;
        }

        public void ResetBowAndArrow()
        {
            _spineBone.localRotation = _spineBoneInitialRotation;

            _bowStringBone.localPosition = _bowStringBoneInitialPose.position;
            _bowStringBone.localRotation = _bowStringBoneInitialPose.rotation;

            _currentInputVector = Vector2.zero;
        }

        public void AdjustAimRotation(Vector2 input)
        {
            input = input * _inputSensitivity * Time.deltaTime;
            _currentInputVector = new Vector3(Mathf.Clamp(_currentInputVector.x + input.x, -_inputHorizontalBound, _inputHorizontalBound),
                Mathf.Clamp(_currentInputVector.y + input.y, -_inputVerticalBound, _inputVerticalBound),
                0f);
        }

        public void CalculateArrowPath()
        {
            Ray ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

            if(Physics.Raycast(ray, Mathf.Infinity, _enemyLayer))
            {
                OnHitDetected?.Invoke(true);
            }
            else
            {
                OnHitDetected?.Invoke(false);
            }
        }

        public void ShootArrow()
        {
            _arrow.transform.SetParent(null);

            Ray ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

            if(Physics.Raycast(ray, out RaycastHit _aimHit, Mathf.Infinity))
            {
                _arrow.transform.LookAt(_aimHit.point);
            }

            _arrow.StartMovement();

            _arrowCount--;
            OnArrowCountUpdated?.Invoke(_arrowCount);

            OnShoot?.Invoke();
        }

        private void MissEnemy()
        {
            OnEnemyMissed?.Invoke(_archer.MovementController.CurrentWaypointIndex - 1);

            if(_arrowCount <= 0)
            {
                GameManager.Instance.LoseGame();
            }
        }

        private void HitEnemy()
        {
            if(_arrowCount <= 0)
            {
                GameManager.Instance.LoseGame();
            }
            else
            {
                _archer.StateMachine.ChangeState(_archer.StateMachine.IdleState);
            }
        }

        private void KillEnemy()
        {
            Sequence killEnemySequence = DOTween.Sequence();
            killEnemySequence.AppendInterval(1.5f);
            killEnemySequence.AppendCallback(() =>
            {
                if(_arrowCount <= 0 && _archer.MovementController.CurrentWaypointIndex < _archer.MovementController.FinalWaypointIndex)
                {
                    GameManager.Instance.LoseGame();
                }
                else if(_archer.MovementController.CurrentWaypointIndex == _archer.MovementController.FinalWaypointIndex)
                {
                    GameManager.Instance.WinGame();
                }
                else
                {
                    _archer.StateMachine.ChangeState(_archer.StateMachine.MoveState);
                }
            });
        }
    }
}