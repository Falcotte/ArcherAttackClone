using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcherAttack.Game
{
    public class LoadingManager : MonoSingleton<LoadingManager>
    {
        [SerializeField] private List<string> _permanentScenes;

        protected new void Awake()
        {
            base.Awake();
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            LoadPermanentScenes();
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