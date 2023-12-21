using UnityEngine;

namespace ArcherAttack.Game.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ArcherAttack/Data/PlayerData", order = 2)]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Stats")]
        [SerializeField] private int _level;
        [SerializeField] private int _currency;

#if UNITY_EDITOR
        public int Level { get => _level; set => _level = value; }
        public int Currency { get => _currency; set => _currency = value; }
#else
        public int Level
        {
            get => PlayerPrefs.GetInt("Level", _level);
            set => PlayerPrefs.SetInt("Level", value);
        }

        public int Currency
        {
            get => PlayerPrefs.GetInt("Currency", _currency);
            set => PlayerPrefs.SetInt("Currency", value);
        }
#endif
    }
}