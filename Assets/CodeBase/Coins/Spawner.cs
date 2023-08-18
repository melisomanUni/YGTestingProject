using System.Collections;
using CodeBase.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Coins
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _maxSpawnPositionY;
        [SerializeField] private float _minSpawnPositionY;
        [SerializeField] private float _offsetSpawnPositionZ;

        private ObjectPool _objectPool;
        private float _spawnInterval;
        private WaitForSeconds _spawnDelay;
        private Coroutine _spawnRoutine;
        private bool _isSpawnAllowed;

        public void Initialize(ObjectPool objectPool, float spawnInterval)
        {
            _objectPool = objectPool;
            _spawnDelay = new WaitForSeconds(spawnInterval);
        }

        private void OnDestroy()
        {
            StopSpawn();
        }

        public void StartSpawn()
        {
            StopSpawn();
            _isSpawnAllowed = true;
            _spawnRoutine = StartCoroutine(Spawn());
        }

        public void StopSpawn()
        {
            _isSpawnAllowed = false;
            
            if (_spawnRoutine != null) 
                StopCoroutine(_spawnRoutine);
        }

        private IEnumerator Spawn()
        {
            while (_isSpawnAllowed)
            {
                if (_objectPool.TryGetObject(out GameObject prefab))
                {
                    float spawnPositionY = Random.Range(_maxSpawnPositionY, _minSpawnPositionY);
                    Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, _offsetSpawnPositionZ);
                    prefab.transform.position = spawnPoint;
                    prefab.SetActive(true);
                }

                yield return _spawnDelay;
            }
        }
    }
}