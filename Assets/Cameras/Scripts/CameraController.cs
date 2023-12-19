using ArcherAttack.Archer;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

namespace ArcherAttack.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _followCamera;
        [SerializeField] private CinemachineVirtualCamera _aimCamera;
        [SerializeField] private CinemachineVirtualCamera _shootCamera;

        [SerializeField] private float _shootCameraFollowDistance;
        [SerializeField] private float _shootCameraFollowDuration;

        private CinemachineTrackedDolly _dolly;

        private void Awake()
        {
            _dolly = _followCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }

        private void OnEnable()
        {
            ArcherMovementController.OnMovementStarted += SwitchToFollowCamera;
            ArcherMovementController.OnWaypointProgression += AdjustCameraPosition;

            ArcherController.OnAimed += SwitchToAimCamera;

            ArcherShooterController.OnShoot += SwitchToShootCamera;
        }

        private void OnDisable()
        {
            ArcherMovementController.OnMovementStarted -= SwitchToFollowCamera;
            ArcherMovementController.OnWaypointProgression -= AdjustCameraPosition;

            ArcherController.OnAimed -= SwitchToAimCamera;

            ArcherShooterController.OnShoot -= SwitchToShootCamera;
        }

        private void AdjustCameraPosition(float pathPosition)
        {
            _dolly.m_PathPosition = pathPosition;
        }

        private void SwitchToAimCamera()
        {
            _aimCamera.Priority = 10;
            _followCamera.Priority = 0;
            _shootCamera.Priority = 0;
        }

        private void SwitchToFollowCamera()
        {
            _followCamera.Priority = 10;
            _aimCamera.Priority = 0;
            _shootCamera.Priority = 0;
        }

        private void SwitchToShootCamera()
        {
            _shootCamera.Priority = 10;
            _followCamera.Priority = 0;
            _aimCamera.Priority = 0;

            AnimateShootCamera();
        }

        private void AnimateShootCamera()
        {
            _shootCamera.transform.position = _aimCamera.transform.position;
            _shootCamera.transform.rotation = _aimCamera.transform.rotation;

            Sequence shootCameraSequence = DOTween.Sequence();
            shootCameraSequence.Append(_shootCamera.transform.DOLocalMove(_shootCamera.transform.localPosition + Vector3.forward * _shootCameraFollowDistance, _shootCameraFollowDuration)
                .SetEase(Ease.InSine));
            shootCameraSequence.AppendCallback(() => SwitchToFollowCamera());
        }
    }
}