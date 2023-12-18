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

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(_currentWaypointIndex != _waypointManager.WaypointCount - 1)
                {
                    MoveToNextWaypoint();
                }
            }
        }

        private void MoveToNextWaypoint()
        {
            _currentWaypointIndex++;
            _agent.SetDestination(_waypointManager.GetWaypoint(_currentWaypointIndex).transform.position);
        }
    }
}