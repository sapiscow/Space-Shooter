using UnityEngine;

namespace Agate.SpaceShooter
{
    public class EnemyBoss : Enemy
    {
        [SerializeField] private float _health = 10;

        protected override void Shoot()
        {
            for (int i = -1; i <= 1; i++)
            {
                EnemyBullet bullet = PoolManager.Instance.GetOrCreateEnemyBullet();
                bullet.transform.position = new Vector2(transform.position.x + i, transform.position.y);
            }
        }

        public override void Shooted()
        {
            _health--;
            if (_health <= 0)
            {
                base.Shooted();
                GameManager.Instance.SetGameEnd();
            }
        }
    }
}