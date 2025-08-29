using Level;
using Services;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent OnLevelStart;
        public UnityEvent OnLevelEnd;
        public UnityEvent OnLevelWin;
        public UnityEvent OnLevelLose;

        void Start()
        {
            OnLevelStart?.Invoke();
        }

        public void WinLevel()
        {
            // start cinamachine to do level transition
            OnLevelEnd?.Invoke();
            OnLevelWin?.Invoke();

            ServiceLocator.Instance.LevelManager.FinishLevel(() => { OnLevelStart?.Invoke(); });
        }

        public void LoseLevel()
        {
            // start cinamachine to do end game\
            OnLevelEnd?.Invoke();
            OnLevelLose?.Invoke();
        }

        public LevelType ChooseNextLevel()
        {
            if (ServiceLocator.Instance.LevelManager.CurrentLevelNumber % 10 == 0)
            {
                return LevelType.PowerUp;
            }
            if (ServiceLocator.Instance.LevelManager.CurrentLevelNumber % 5 == 0)
            {
                return LevelType.PowerUp;
            }
            return new []{LevelType.ReachFinish, LevelType.Survival, LevelType.KillAllEnemies}[Random.Range(0,3)];

        }
    }
}
