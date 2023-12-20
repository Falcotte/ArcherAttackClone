using System.Collections.Generic;
using UnityEngine;

namespace ArcherAttack.Game
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<string> _levels = new List<string>();
        public List<string> Levels => _levels;
    }
}