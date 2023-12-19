using ArcherAttack.Inputs;
using UnityEngine;

namespace ArcherAttack.Archer
{
    public class ArcherShooterController : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _spineBone;

        [SerializeField] private Transform _arrow;
        [SerializeField] private Transform _arrowBone;

        [SerializeField] private Transform _bowStringBone;

        [SerializeField] private Pose _arrowBoneFinalPose;
        [SerializeField] private Pose _bowStringBoneFinalPose;

        [SerializeField] private float _inputSensitivity;
        [SerializeField] private float _inputHorizontalBound;
        [SerializeField] private float _inputVerticalBound;

        private Pose _arrowBoneInitialPose;
        private Pose _bowStringBoneInitialPose;

        private Vector2 _currentInputVector;

        private void Awake()
        {
            _arrowBone.GetLocalPositionAndRotation(out Vector3 arrowBonePosition, out Quaternion arrowBoneRotation);
            _bowStringBoneInitialPose = new Pose(arrowBonePosition, arrowBoneRotation);

            _bowStringBone.GetLocalPositionAndRotation(out Vector3 bowStringBonePosition, out Quaternion bowStringBoneRotation);
            _bowStringBoneInitialPose = new Pose(bowStringBonePosition, bowStringBoneRotation);
        }

        private void OnEnable()
        {
            InputController.OnDragDelta += AdjustAimRotation;
        }

        private void OnDisable()
        {
            InputController.OnDragDelta -= AdjustAimRotation;
        }

        private void LateUpdate()
        {
            _spineBone.Rotate(new Vector3(0f, _currentInputVector.x, -_currentInputVector.y), Space.Self);
        }

        public void AnimateBowAndArrow()
        {
            _arrow.gameObject.SetActive(true);

            _arrowBone.localPosition = _arrowBoneFinalPose.position;
            _arrowBone.localRotation = _arrowBoneFinalPose.rotation;

            _bowStringBone.localPosition = _bowStringBoneFinalPose.position;
            _bowStringBone.localRotation = _bowStringBoneFinalPose.rotation;
        }

        public void ResetBowAndArrow()
        {
            _arrow.gameObject.SetActive(false);

            _arrowBone.localPosition = _arrowBoneInitialPose.position;
            _arrowBone.localRotation = _arrowBoneInitialPose.rotation;

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
    }
}