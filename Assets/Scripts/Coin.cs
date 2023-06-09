using UnityEngine;

namespace Agate.SpaceShooter
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        
        private Rigidbody2D _rigidbody;
        private Camera _mainCamera;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _mainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            Vector2 moveTarget = _rigidbody.position + Vector2.down * (_speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(moveTarget);

            if (transform.position.y < -_mainCamera.orthographicSize)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                GameManager.Instance.AddScore(20);
                gameObject.SetActive(false);
            }
        }
    }
}