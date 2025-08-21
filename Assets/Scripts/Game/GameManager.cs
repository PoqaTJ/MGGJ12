using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnLevelStart;
    public UnityEvent OnLevelEnd;
    public UnityEvent OnLevelWin;
    public UnityEvent OnLevelLose;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        OnLevelStart?.Invoke();
    }
    
    public void WinLevel()
    {
        // start cinamachine to do level transition
        OnLevelEnd?.Invoke();
        OnLevelWin?.Invoke();
        
        OnLevelStart?.Invoke();
    }

    public void LoseLevel()
    {
        // start cinamachine to do end game\
        OnLevelEnd?.Invoke();
        OnLevelLose?.Invoke();
    }
}
