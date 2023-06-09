using UnityEngine;

namespace Agate.SpaceShooter
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private Transform[] _backgrounds;

        private void LateUpdate()
        {
            foreach (Transform background in _backgrounds)
            {
                background.Translate(0f, -Time.deltaTime, 0f);
                if (background.localPosition.y <= -19) // Bottom position outside camera
                {
                    background.transform.localPosition = new Vector2(
                        background.localPosition.x,
                        transform.GetChild(0).localPosition.y + 17 // 17 is best delta position each background
                    );
                    background.SetAsFirstSibling();
                }
            }
        }
    }
}