using ArcherAttack.Waypoints;
using UnityEngine;
using UnityEngine.AI;

namespace ArcherAttack.Archer
{
    public class ArcherController : MonoBehaviour
    {
        [SerializeField] private WaypointManager _waypointManager;

        [SerializeField] private NavMeshAgent _agent;

        private int _currentWaypointIndex;
        public Vector3 CurrentDestination => _waypointManager.GetWaypoint(_currentWaypointIndex).transform.position;

        public void MoveToNextWaypoint()
        {
            if(_currentWaypointIndex != _waypointManager.WaypointCount - 1)
            {
                _currentWaypointIndex++;
                _agent.SetDestination(CurrentDestination);
            }
        }
    }
}