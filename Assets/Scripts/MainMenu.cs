using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Agate.SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("Gameplay");
        }

        private void OnCloseButtonClicked()
        {
            Application.Quit();
        }
    }
}