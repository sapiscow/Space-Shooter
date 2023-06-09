using UnityEngine;

namespace Agate.SpaceShooter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _health = 5;
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _moveMaxDistance = 4f;
        [SerializeField] private float _shootDelay = 1f;

        [Header("UI")]
        [SerializeField] private RectTransform _playerHealth;
        [SerializeField] private GameObject _playerHealthElementPrefab;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private int _moveDirection;
        private float _shootDelayCounter;

        private GameObject[] _playerHealthElements;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _playerHealthElements = new GameObject[_health];
            for (int i = 0; i < _health; i++)
            {
                _playerHealthElements[i] = Instantiate(_playerHealthElementPrefab, _playerHealth);
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            else
            {
                StopMove();
            }
#endif

            _animator.SetInteger("moveDirection", _moveDirection);
        }

        private void FixedUpdate()
        {
            Vector2 moveTarget = _rigidbody.position + Vector2.right * (_moveDirection * _moveSpeed * Time.fixedDeltaTime);
            moveTarget.x = Mathf.Clamp(moveTarget.x, -_moveMaxDistance, _moveMaxDistance);

            _rigidbody.MovePosition(moveTarget);

            _shootDelayCounter += Time.fixedDeltaTime;
            if (_shootDelayCounter > _shootDelay)
            {
                _shootDelayCounter = 0f;

                PlayerBullet bullet = PoolManager.Instance.GetOrCreatePlayerBullet();
                bullet.transform.position = transform.position;
            }
        }

        public void MoveLeft()
        {
            _moveDirection = -1;
        }

        public void MoveRight()
        {
            _moveDirection = 1;
        }

        public void StopMove()
        {
            _moveDirection = 0;
        }

        public void Shooted()
        {
            if (_health <= 0)
            {
                return;
            }
            
            _health--;
            _playerHealthElements[_health].SetActive(false);

            if (_health <= 0)
            {
                Destroy(gameObject);
                GameManager.Instance.SetGameEnd();
            }
        }
    }
}