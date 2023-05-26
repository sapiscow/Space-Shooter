using UnityEngine;

namespace Agate.SpaceShooter
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float _moveSpeed = 1f;
        [SerializeField] protected Vector2 _moveMaxDistance = new Vector2(4f, 3f);
        [SerializeField] protected Vector2 _changeDirectionDelay = new Vector2(1f, 2f);
        [SerializeField] protected Vector2 _enemyShootDelay = new Vector2(1f, 2f);
        [SerializeField] protected EnemyBullet _bulletPrefab;

        protected Rigidbody2D _rigidbody;
        protected float _moveDirection;
        protected float _changeDirectionDelayCounter;
        protected float _enemyShootDelayCounter;

        protected void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        protected void OnEnable()
        {
            RandomizeDirection();
        }

        protected void FixedUpdate()
        {
            float moveSpeed = _moveSpeed * Time.fixedDeltaTime;
            Vector2 moveTarget = _rigidbody.position + new Vector2(_moveDirection * moveSpeed, -moveSpeed);
            
            moveTarget.x = Mathf.Clamp(moveTarget.x, -_moveMaxDistance.x, _moveMaxDistance.x);

            // Just like clamping value, enemy can't go lower than certain y position
            if (transform.position.y < _moveMaxDistance.y)
            {
                moveTarget.y = transform.position.y;
            }

            _rigidbody.MovePosition(moveTarget);

            _changeDirectionDelayCounter -= Time.fixedDeltaTime;
            if (_changeDirectionDelayCounter < 0f)
            {
                RandomizeDirection();
                _changeDirectionDelayCounter = Random.Range(_changeDirectionDelay.x, _changeDirectionDelay.y);
            }

            _enemyShootDelayCounter -= Time.fixedDeltaTime;
            if (_enemyShootDelayCounter < 0f)
            {
                Shoot();
                _enemyShootDelayCounter = Random.Range(_enemyShootDelay.x, _enemyShootDelay.y);
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.RemoveEnemyFromList(this);
        }

        protected void RandomizeDirection()
        {
            _moveDirection = Random.value > 0.5f ? 1 : -1;
        }

        protected virtual void Shoot()
        {
            EnemyBullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        }

        public virtual void Shooted()
        {
            Destroy(gameObject);
        }
    }
}