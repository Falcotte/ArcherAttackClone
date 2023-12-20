using System.Collections.Generic;
using UnityEngine;

namespace ArcherAttack.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<EnemyController> _enemies = new();

        public EnemyController GetEnemy(int index)
        {
            if(index < 0 || index >= _enemies.Count)
                return null;

            return _enemies[index];
        }
    }
}