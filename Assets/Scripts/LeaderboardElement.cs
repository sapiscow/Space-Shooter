using TMPro;
using UnityEngine;

namespace Agate.SpaceShooter
{
    public class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _score;

        public void SetData(PlayerDataElement data)
        {
            _name.SetText(data.GetName());
            _score.SetText(data.GetScore().ToString());
        }
    }
}