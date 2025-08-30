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

        [SerializeField] private Transform levelRoot;
        [SerializeField] private float levelSize = 10;
        private float nextLevelLocation = 0;

        private LevelController currentLevel = null;
        private LevelController nextLevel = null;
        private LevelController previousLevel = null;

        private List<LevelController> levelsToDestroy = new();


        private void Start()
        {
            levelRoot = new GameObject("LevelRoot").transform;
            ServiceLocator.Instance.GameManager.OnLevelStart.AddListener(OnScrollToNewLevelFinished);
            currentLevel = SetupLevel(ServiceLocator.Instance.GameManager.ChooseNextLevel());
            nextLevel = SetupLevel(ServiceLocator.Instance.GameManager.ChooseNextLevel());
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
            if (previousLevel != null)
            {
                levelsToDestroy.Add(previousLevel);
            }
            previousLevel = currentLevel;
            currentLevel = nextLevel;

            // Load new next level
            LevelType nextLevelType = ServiceLocator.Instance.GameManager.ChooseNextLevel();
            nextLevel = SetupLevel(nextLevelType);

            // Start next level
            CurrentLevelNumber += 1;
            
            ServiceLocator.Instance.CameraManager.ScrollTo(currentLevel.transform.position, () =>
            {
                callback?.Invoke();
            });
        }
        
        // temp
        [SerializeField] private GameObject survivalLevel;
        [SerializeField] private GameObject killEnemiesLevel;
        [SerializeField] private GameObject raceLevel;
        
        private LevelController SetupLevel(LevelType levelType)
        {
            GameObject prefab = null;
            switch (levelType)
            {
                case LevelType.ReachFinish:
                    prefab = raceLevel;
                    break;
                case LevelType.KillAllEnemies:
                    prefab = killEnemiesLevel;
                    break;
                case LevelType.Survival:
                    prefab = survivalLevel;
                    break;
            }

            if (prefab != null)
            {
                GameObject levelObj = Instantiate(prefab, levelRoot, true);
                var position = levelObj.transform.position;
                position = new Vector3(position.x, nextLevelLocation,
                    position.z);
                nextLevelLocation += levelSize;
                levelObj.transform.position = position;
                return levelObj.GetComponent<LevelController>();
            }

            return null;
        }
    }
}