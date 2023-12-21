using UnityEngine;
using UnityEngine.Events;

namespace ArcherAttack.Game
{
    public enum GameState
    {
        MainMenu,
        Gameplay,
        GameWin,
        GameLose
    }

    public class GameManager : MonoSingleton<GameManager>
    {
        public static UnityAction OnGameStart;
        public static UnityAction OnGameWin, OnGameLose;
        public static UnityAction<GameState> OnGameStateChange;

        public GameState InitialState = GameState.MainMenu;

        private GameState _currentState;
        public GameState CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        private new void Awake()
        {
            base.Awake();

            _currentState = InitialState;
        }

        public void ChangeState(GameState gameState)
        {
            _currentState = gameState;
            Debug.Log($"GameState changed -> [{_currentState}]");

            OnGameStateChange?.Invoke(gameState);
        }

        private void GoToInitialState()
        {
            ChangeState(InitialState);
        }

        public void OpenMainMenu()
        {
            ChangeState(GameState.MainMenu);
        }

        public void StartGame()
        {
            if(CurrentState == GameState.Gameplay)
                return;

            ChangeState(GameState.Gameplay);
            OnGameStart?.Invoke();
        }

        public void WinGame()
        {
            OnGameWin?.Invoke();
            ChangeState(GameState.GameWin);
        }

        public void LoseGame()
        {
            OnGameLose?.Invoke();
            ChangeState(GameState.GameLose);
        }
    }
}
