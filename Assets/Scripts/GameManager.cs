using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agate.SpaceShooter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Enemy")]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private EnemyBoss _enemyBossPrefab;
        [SerializeField] private int _enemyCount = 5;
        [SerializeField] private float _enemySpawnRange = 4;
        [SerializeField] private float _enemySpawnDelay = 1f;
        [SerializeField] private float _bossIncomingDelay = 10f;

        [Header("Coin")]
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private float _coinSpawnRange = 4;

        [Header("UI")]
        [SerializeField] private TMP_Text _scoreText;

        private Camera _mainCamera;
        private int _score;
        private bool _isSpawning = true;
        private float _enemySpawnDelayCounter;
        private List<Enemy> _spawnedEnemies = new();

        private void Awake()
        {
            Instance = this;

            _mainCamera = Camera.main;
        }

        private void Start()
        {
            SpawnCoin();
        }

        private void FixedUpdate()
        {
            if (_isSpawning)
            {
                _enemySpawnDelayCounter += Time.fixedDeltaTime;
                if (_enemySpawnDelayCounter > _enemySpawnDelay)
                {
                    SpawnEnemies();
                    _enemySpawnDelayCounter = 0f;
                    _isSpawning = false;
                }
            }

            if (_bossIncomingDelay > 0f)
            {
                _bossIncomingDelay -= Time.fixedDeltaTime;
                if (_bossIncomingDelay <= 0f)
                {
                    EnemyBoss enemyBoss = Instantiate(_enemyBossPrefab);
                    enemyBoss.transform.position = new Vector3(0f, _mainCamera.orthographicSize, 0f);
                }
            }
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                Enemy enemy = Instantiate(_enemyPrefab);
                enemy.transform.position = new Vector3(
                    Random.Range(-_enemySpawnRange, _enemySpawnRange), _mainCamera.orthographicSize, 0f
                );

                _spawnedEnemies.Add(enemy);
            }
        }

        private void SpawnCoin()
        {
            Coin coin = Instantiate(_coinPrefab);
            coin.transform.position = new Vector3(
                Random.Range(-_coinSpawnRange, _coinSpawnRange), _mainCamera.orthographicSize, 0f
            );
        }

        public void AddScore(int value)
        {
            _score += value;
            _scoreText.SetText(_score.ToString());
        }

        public void RemoveEnemyFromList(Enemy enemy)
        {
            _spawnedEnemies.Remove(enemy);
            if (_spawnedEnemies.Count <= 0)
            {
                _isSpawning = true;
            }
        }

        public void SetGameEnd()
        {
            SceneManager.LoadScene(0);
        }
    }
}