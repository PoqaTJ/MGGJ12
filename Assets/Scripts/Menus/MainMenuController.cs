using Game;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menus
{
    public class MainMenuController: MonoBehaviour
    {
        [SerializeField] private Button playButton;
        private GameManager gameManager;
        
        private void Start()
        {
            gameManager = ServiceLocator.Instance.GameManager;
        }

        public void OnPressPlay()
        {
            playButton.interactable = false;
            SceneManager.LoadScene("Gameplay");
        }
    }
}