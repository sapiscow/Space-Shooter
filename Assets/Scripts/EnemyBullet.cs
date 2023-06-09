using UnityEngine;

namespace Agate.SpaceShooter
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 8f;
        [SerializeField] private float _maxDistance = 8f;
        
        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 moveTarget = _rigidbody.position + Vector2.down * (_speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(moveTarget);

            if (transform.position.y < -_maxDistance)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                gameObject.SetActive(false);
                player.Shooted();
            }
        }
    }
}