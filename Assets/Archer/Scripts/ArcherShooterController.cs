using ArcherAttack.Enemy;
using ArcherAttack.Inputs;
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

        private ArrowController _arrow;
        private Pose _bowStringBoneInitialPose;

        private Vector2 _currentInputVector;

        public static UnityAction OnShoot;
        public static UnityAction<int> OnEnemyMissed;

        private void Awake()
        {
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
            _bowStringBone.localPosition = _bowStringBoneInitialPose.position;
            _bowStringBone.localRotation = _bowStringBoneInitialPose.rotation;

            _currentInputVector = Vector2.zero;
        }

        public void AdjustAimRotation(Vector2 input)
        {
            input = input * _inputSensitivity * Time.deltaTime;
            _currentInputVector = new Vector3(Mathf.Clamp(_currentInputVector.x + input.x,-_inputHorizontalBound,_inputHorizontalBound),
                Mathf.Clamp(_currentInputVector.y + input.y, -_inputVerticalBound, _inputVerticalBound),
                0f);
        }

        public void CalculateArrowPath()
        {
            Debug.DrawLine(_shootPoint.position, _shootPoint.position + _shootPoint.forward * 50f);
        }

        public void ShootArrow()
        {
            _arrow.transform.SetParent(null);
            _arrow.StartMovement();

            OnShoot?.Invoke();
        }

        private void MissEnemy()
        {
            OnEnemyMissed?.Invoke(_archer.MovementController.CurrentWaypointIndex - 1);
        }

        private void HitEnemy()
        {
            _archer.StateMachine.ChangeState(_archer.StateMachine.IdleState);
        }

        private void KillEnemy()
        {
            _archer.StateMachine.ChangeState(_archer.StateMachine.MoveState);
        }
    }
}