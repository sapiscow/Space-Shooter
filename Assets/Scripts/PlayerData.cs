using System.Collections.Generic;
using UnityEngine;

namespace Agate.SpaceShooter
{
    [System.Serializable]
    public class PlayerData
    {
        private const int _maxScoreCount = 3;

        [SerializeField] private List<PlayerDataElement> _scores = new();

        public void AddScore(string name, int score)
        {
            int index = 0;
            for (int i = _scores.Count - 1; i >= 0; i--)
            {
                if (score <= _scores[i].GetScore())
                {
                    index = i + 1;
                    break;
                }
            }

            _scores.Insert(index, new PlayerDataElement(name, score));

            if (_scores.Count > _maxScoreCount)
            {
                _scores.RemoveRange(_maxScoreCount, Mathf.Max(0, _scores.Count - _maxScoreCount));
            }

            Save();
        }

        public PlayerDataElement[] GetAllScore()
        {
            return _scores.ToArray();
        }

        public void PrintAllScores()
        {
            Debug.Log("----- Player Leaderboard -----");
            foreach (PlayerDataElement data in _scores)
            {
                Debug.Log(data.GetName() + " " + data.GetScore());
            }
            Debug.Log("------------------------------");
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString("PlayerData", json);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey("PlayerData"))
            {
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerData"), this);
            }
        }
    }

    [System.Serializable]
    public class PlayerDataElement
    {
        [SerializeField] private string _name;
        [SerializeField] private int _score;

        public PlayerDataElement(string name, int score)
        {
            _name = name;
            _score = score;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetScore()
        {
            return _score;
        }
    }
}