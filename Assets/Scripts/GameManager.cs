using TMPro;
using UnityEngine;

namespace Agate.SpaceShooter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _enemyCount = 5;
        [SerializeField] private float _enemySpawnRange = 4;

        [Header("UI")]
        [SerializeField] private TMP_Text _scoreText;

        private int _score;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                Enemy enemy = Instantiate(_enemyPrefab);
                enemy.transform.position = new Vector3(
                    Random.Range(-_enemySpawnRange, _enemySpawnRange), Camera.main.orthographicSize, 0f
                );
            }
        }

        public void AddScore(int value)
        {
            _score += value;
            _scoreText.SetText(_score.ToString());
        }
    }
}