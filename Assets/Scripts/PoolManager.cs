using System.Collections.Generic;
using UnityEngine;

namespace Agate.SpaceShooter
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;

        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private PlayerBullet _playerBulletPrefab;
        [SerializeField] private EnemyBullet _enemyBulletPrefab;
        [SerializeField] private Coin _coinPrefab;

        private List<Enemy> _enemyPool = new();
        private List<PlayerBullet> _playerBulletPool = new();
        private List<EnemyBullet> _enemyBulletPool = new();
        private List<Coin> _coinPool = new();

        private void Awake()
        {
            Instance = this;
        }

        public Enemy GetOrCreateEnemy()
        {
            Enemy enemy = _enemyPool.Find(o => !o.gameObject.activeSelf);
            if (enemy == null)
            {
                enemy = Instantiate(_enemyPrefab);
                _enemyPool.Add(enemy);
            }

            enemy.gameObject.SetActive(true);

            return enemy;
        }

        public PlayerBullet GetOrCreatePlayerBullet()
        {
            PlayerBullet bullet = _playerBulletPool.Find(o => !o.gameObject.activeSelf);
            if (bullet == null)
            {
                bullet = Instantiate(_playerBulletPrefab);
                _playerBulletPool.Add(bullet);
            }

            bullet.gameObject.SetActive(true);

            return bullet;
        }

        public EnemyBullet GetOrCreateEnemyBullet()
        {
            EnemyBullet bullet = _enemyBulletPool.Find(o => !o.gameObject.activeSelf);
            if (bullet == null)
            {
                bullet = Instantiate(_enemyBulletPrefab);
                _enemyBulletPool.Add(bullet);
            }

            bullet.gameObject.SetActive(true);

            return bullet;
        }

        public Coin GetOrCreateCoin()
        {
            Coin coin = _coinPool.Find(o => !o.gameObject.activeSelf);
            if (coin == null)
            {
                coin = Instantiate(_coinPrefab);
                _coinPool.Add(coin);
            }

            coin.gameObject.SetActive(true);

            return coin;
        }
    }
}