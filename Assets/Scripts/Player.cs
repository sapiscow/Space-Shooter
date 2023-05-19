using UnityEngine;

namespace Agate.SpaceShooter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _moveMaxDistance = 4f;
        [SerializeField] private float _shootDelay = 1f;
        [SerializeField] private PlayerBullet _bulletPrefab;

        private Rigidbody2D _rigidbody;
        private float _moveDirection;
        private float _shootDelayCounter;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
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
                PlayerBullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
            }
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
        }
    }
}