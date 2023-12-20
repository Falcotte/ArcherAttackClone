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

        private GameState currentState;
        public GameState CurrentState
        {
            get
            {
                return currentState;
            }
        }

        private new void Awake()
        {
            base.Awake();

            currentState = InitialState;
        }

        public void ChangeState(GameState gameState)
        {
            currentState = gameState;
            Debug.Log($"GameState changed -> [{currentState}]");

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
