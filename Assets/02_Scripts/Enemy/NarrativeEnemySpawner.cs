using System;
using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _02_Scripts.Enemy
{
    public class NarrativeEnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private int enemyCount;
        public event Action OnBattleFinished;
        private Player _player;
        private List<GameObject> _spawnedEnemies = new List<GameObject>();

        void Start()
        {
            _player = GameManager.Instance.Player;
        }

        public IEnumerator SpawnEnemyBulk()
        {
            _spawnedEnemies.Clear();

            for (int i = 0; i < enemyCount; i++)
            {
                var random = Random.Range(1, 10);
                GameObject spawnedEnemy = Instantiate(enemy, _player.transform.position + new Vector3(random, 0, 10), Quaternion.identity);
                _spawnedEnemies.Add(spawnedEnemy);

                yield return null;
            }

            StartCoroutine(CheckEnemiesDestroyed());
        }

        private IEnumerator CheckEnemiesDestroyed()
        {
            while (_spawnedEnemies.Count > 0)
            {
                for (int i = _spawnedEnemies.Count - 1; i >= 0; i--)
                {
                    if (_spawnedEnemies[i] == null)
                    {
                        _spawnedEnemies.RemoveAt(i);
                    }
                }

                yield return new WaitForSeconds(0.5f);
            }
            OnBattleFinished?.Invoke();
        }
    }
}