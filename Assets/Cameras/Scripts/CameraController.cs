using ArcherAttack.Archer;
using Cinemachine;
using UnityEngine;

namespace ArcherAttack.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _followCamera;
        [SerializeField] private CinemachineVirtualCamera _aimCamera;
        [SerializeField] private CinemachineVirtualCamera _shootCamera;

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
        }

        private void OnDisable()
        {
            ArcherMovementController.OnMovementStarted -= SwitchToFollowCamera;
            ArcherMovementController.OnWaypointProgression -= AdjustCameraPosition;

            ArcherController.OnAimed -= SwitchToAimCamera;
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

        private void SwitchToShootCamera(Transform arrow)
        {
            _shootCamera.Priority = 10;
            _followCamera.Priority = 0;
            _aimCamera.Priority = 0;
        }
    }
}