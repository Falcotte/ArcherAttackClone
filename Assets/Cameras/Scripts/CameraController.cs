using ArcherAttack.Archer;
using Cinemachine;
using UnityEngine;

namespace ArcherAttack.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        private CinemachineTrackedDolly _dolly;

        private void Awake()
        {
            _dolly = _camera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }

        private void OnEnable()
        {
            ArcherController.OnWaypointProgression += AdjustCameraPosition;
        }

        private void OnDisable()
        {
            
        }

        private void AdjustCameraPosition(float pathPosition)
        {
            _dolly.m_PathPosition = pathPosition;
        }
    }
}