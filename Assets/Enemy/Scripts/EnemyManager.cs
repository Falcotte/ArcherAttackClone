using System.Collections.Generic;
using UnityEngine;

namespace ArcherAttack.Enemy
{
    public class EnemyManager : MonoSingleton<EnemyManager>
    {
        [SerializeField] private List<EnemyController> _enemies = new();

        public EnemyController GetEnemy(int index)
        {
            return _enemies[index];
        }
    }
}