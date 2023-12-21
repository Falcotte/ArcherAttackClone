using System.Collections.Generic;
using ArcherAttack.Game.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

namespace ArcherAttack.Game
{
    public class LoadingManager : MonoSingleton<LoadingManager>
    {
        [SerializeField] private List<string> _permanentScenes;

        [SerializeField] private LevelManager _levelManager;

        private string _currentLoadedLevel;

        protected new void Awake()
        {
            base.Awake();
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            LoadPermanentScenes();
            LoadLevel(0);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SetActiveScene;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SetActiveScene;
        }

        #region Loading

        private void LoadPermanentScenes()
        {
            foreach(var scene in _permanentScenes)
            {
                if(!IsSceneOpen(scene))
                {
                    Debug.Log($"{scene} loading");
                    SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                }
            }
        }

        private void LoadLevel(int levelIndex)
        {
            int levelToLoadIndex = levelIndex % _levelManager.Levels.Count;

            SceneManager.LoadScene(_levelManager.Levels[levelToLoadIndex], LoadSceneMode.Additive);
            _currentLoadedLevel = _levelManager.Levels[levelToLoadIndex];
        }

        public void LoadNextLevel()
        {
            SceneManager.UnloadSceneAsync(_currentLoadedLevel);

            DataManager.PlayerData.Level++;
            LoadLevel(DataManager.PlayerData.Level);
        }

        public void ReloadLevel()
        {
            SceneManager.UnloadSceneAsync(_currentLoadedLevel);
            SceneManager.LoadScene(_currentLoadedLevel, LoadSceneMode.Additive);
        }

        #endregion

        #region Helpers

        private void SetActiveScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.SetActiveScene(scene);
        }

        private bool IsSceneOpen(string name)
        {
            var sceneCount = SceneManager.sceneCount;
            for(int i = 0; i < sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name == name)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}