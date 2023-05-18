using UnityEngine;

namespace Agate.SpaceShooter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private PlayerBullet _bulletPrefab;

        private Rigidbody2D _rigidbody;
        private float _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 moveTarget = _rigidbody.position + Vector2.right * (_moveDirection * _moveSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(moveTarget);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _moveDirection = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _moveDirection = 1f;
            }
            else
            {
                _moveDirection = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerBullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
            }
        }
    }
}