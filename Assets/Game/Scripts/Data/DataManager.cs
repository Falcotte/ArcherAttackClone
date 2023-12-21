using UnityEngine;

namespace ArcherAttack.Game.Data
{
    public class DataManager : MonoSingleton<DataManager>
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private GameData _gameData;

        public static PlayerData PlayerData
        {
            get
            {
                return Instance._playerData;
            }
        }

        public static GameData GameData
        {
            get
            {
                return Instance._gameData;
            }
        }
    }
}