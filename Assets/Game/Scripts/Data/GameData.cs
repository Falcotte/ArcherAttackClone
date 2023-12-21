using UnityEngine;

namespace ArcherAttack.Game.Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ArcherAttack/Data/GameData", order = 2)]
    public class GameData : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField] private bool _soundEnabled;
        [SerializeField] private bool _vibrationEnabled;

#if UNITY_EDITOR
        public bool SoundEnabled { get => _soundEnabled; set => _soundEnabled = value; }
        public bool VibrationEnabled { get => _vibrationEnabled; set => _vibrationEnabled = value; }
#else
        public bool SoundEnabled
        {
            get => PlayerPrefsX.GetBool("SoundEnabled", _soundEnabled);
            set => PlayerPrefsX.SetBool("SoundEnabled", value);
        }

        public bool VibrationEnabled
        {
            get => PlayerPrefsX.GetBool("VibrationEnabled", _vibrationEnabled);
            set => PlayerPrefsX.SetBool("VibrationEnabled", value);
        }
#endif
    }
}