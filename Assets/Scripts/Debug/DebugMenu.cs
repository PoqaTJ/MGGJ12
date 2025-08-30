using Game;
using Services;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] private Button winLevelButton;
    [SerializeField] private Button loseLevelButton;
    
    void Start()
    {
        winLevelButton.onClick.AddListener(() =>
        {
            ServiceLocator.Instance.GameManager.WinLevel();
        });
        loseLevelButton.onClick.AddListener(() =>
        {
            ServiceLocator.Instance.GameManager.LoseLevel();
        });
        ServiceLocator.Instance.GameManager.OnLevelStart.AddListener(OnLevelStart);
        ServiceLocator.Instance.GameManager.OnLevelEnd.AddListener(OnLevelEnd);
        
        UpdateButtons(false);
    }

    void UpdateButtons(bool levelInProgress)
    {
        winLevelButton.interactable = levelInProgress;
        loseLevelButton.interactable = levelInProgress;
    }

    void OnLevelStart()
    {
        UpdateButtons(true);
    }

    void OnLevelEnd()
    {
        UpdateButtons(false);
    }

    private void OnDisable()
    {
        ServiceLocator.Instance.GameManager.OnLevelStart.RemoveListener(OnLevelStart);
        ServiceLocator.Instance.GameManager.OnLevelEnd.RemoveListener(OnLevelEnd);
    }
}
