using ArcherAttack.Waypoints;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace ArcherAttack.Archer
{
    public class ArcherMovementController : MonoBehaviour
    {
        [SerializeField] private WaypointManager _waypointManager;

        [SerializeField] private NavMeshAgent _agent;
        public Vector3 CurrentDestination => _waypointManager.GetWaypoint(_currentWaypointIndex).transform.position;

        private int _previousWaypointIndex;
        private int _currentWaypointIndex;
        private float _waypointProgression;

        public static UnityAction<float> OnWaypointProgression;

        private void Start()
        {
            _previousWaypointIndex = _currentWaypointIndex;
        }

        public void MoveToNextWaypoint()
        {
            if(_currentWaypointIndex != _waypointManager.WaypointCount - 1)
            {
                _currentWaypointIndex++;
                _agent.SetDestination(CurrentDestination);
            }
        }

        public void UpdateWaypointProgression()
        {
            Vector3 _previousWaypointPosition = _waypointManager.GetWaypoint(_previousWaypointIndex).transform.position;
            Vector3 _currentWaypointPosition = _waypointManager.GetWaypoint(_currentWaypointIndex).transform.position;

            _waypointProgression = Vector3.Dot(transform.position - _previousWaypointPosition, _currentWaypointPosition - _previousWaypointPosition) /
                Vector3.Dot(_currentWaypointPosition - _previousWaypointPosition, _currentWaypointPosition - _previousWaypointPosition);

            OnWaypointProgression?.Invoke(_previousWaypointIndex + _waypointProgression);
        }

        public void ReachWaypoint()
        {
            _previousWaypointIndex = _currentWaypointIndex;
        }
    }
}