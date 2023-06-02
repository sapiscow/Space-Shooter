using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Agate.SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private RectTransform _leaderboard;
        [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

        private PlayerData _playerData = new PlayerData();

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);

            _playerData.Load();
            foreach (PlayerDataElement data in _playerData.GetAllScore())
            {
                LeaderboardElement element = Instantiate(_leaderboardElementPrefab, _leaderboard);
                element.SetData(data);
            }
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