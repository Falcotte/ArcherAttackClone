using System.Collections.Generic;
using UnityEngine;

namespace ArcherAttack.Waypoints
{
    public class WaypointManager : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> _waypoints = new();

        public int WaypointCount => _waypoints.Count;

        public Waypoint GetWaypoint(int index)
        {
            if(index < 0 || index >= _waypoints.Count)
                return null;

            return _waypoints[index];
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            for(int i = 0; i < _waypoints.Count; i++)
            {
                if(_waypoints[i] == null)
                {
                    _waypoints.RemoveAt(i);
                    break;
                }

                if(i < _waypoints.Count - 1)
                {
                    if(_waypoints[i + 1] == null)
                    {
                        _waypoints.RemoveAt(i + 1);
                        return;
                    }

                    Gizmos.DrawLine(_waypoints[i].transform.position, _waypoints[i + 1].transform.position);
                }

                Gizmos.DrawWireSphere(_waypoints[i].transform.position, .25f);
                UnityEditor.Handles.Label(_waypoints[i].transform.position + Vector3.up, i.ToString());
            }
        }
#endif
    }
}