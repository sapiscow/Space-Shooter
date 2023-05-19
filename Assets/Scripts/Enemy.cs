using UnityEngine;

namespace Agate.SpaceShooter
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private Vector2 _moveMaxDistance = new Vector2(4f, 3f);

        private Rigidbody2D _rigidbody;
        private float _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _moveDirection = Random.value > 0.5f ? 1 : -1;
        }

        private void FixedUpdate()
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
        }
    }
}