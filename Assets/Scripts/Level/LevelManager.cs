using System;
using System.Collections.Generic;
using Services;
using UnityEngine;

namespace Level
{
    public class LevelManager: MonoBehaviour
    {
        public int CurrentLevelNumber { get; private set; } = 1;
        public LevelController CurrentLevel => currentLevel;
        public LevelController NextLevel => nextLevel;
        public LevelController PreviousLevel => previousLevel;

        private LevelController currentLevel = null;
        private LevelController nextLevel = null;
        private LevelController previousLevel = null;

        private List<LevelController> levelsToDestroy = new();


        private void Start()
        {
            ServiceLocator.Instance.GameManager.OnLevelStart.AddListener(OnScrollToNewLevelFinished);
        }

        private void OnScrollToNewLevelFinished()
        {
            foreach (var lc in levelsToDestroy)
            {
                Destroy(lc.gameObject);
            }
            
            levelsToDestroy.Clear();
        }
        
        public void FinishLevel(Action callback)
        {
            // shut down current level

            levelsToDestroy.Add(previousLevel);
            previousLevel = currentLevel;
            currentLevel = nextLevel;

            // Load new next level
            LevelType nextLevelType = ServiceLocator.Instance.GameManager.ChooseNextLevel();
            nextLevel = LoadNextLevel(nextLevelType);
            
            // Start next level
            
            CurrentLevelNumber += 1;
            
            callback?.Invoke();
        }
        
        private LevelController LoadNextLevel(LevelType levelType)
        {
            return null;
        }
    }
}