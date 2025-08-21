using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] private Button winLevelButton;
    [SerializeField] private Button loseLevelButton;
    
    void Start()
    {
        GameManager.Instance.OnLevelStart.AddListener(OnLevelStart);
        GameManager.Instance.OnLevelEnd.AddListener(OnLevelEnd);
        
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

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelStart.RemoveListener(OnLevelStart);
        GameManager.Instance.OnLevelEnd.RemoveListener(OnLevelEnd);

    }
}
