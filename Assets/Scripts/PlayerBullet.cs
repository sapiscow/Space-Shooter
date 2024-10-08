using UnityEngine;

namespace Agate.SpaceShooter
{
    public class PlayerBullet : MonoBehaviour
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
            Vector2 moveTarget = _rigidbody.position + Vector2.up * (_speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(moveTarget);

            if (transform.position.y > _maxDistance)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                GameManager.Instance.AddScore(10);
                gameObject.SetActive(false);
                enemy.Shooted();
            }
        }
    }
}