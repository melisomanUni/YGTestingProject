using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _container;

        private readonly List<GameObject> _pool = new List<GameObject>();

        private int _sizePool;

        public void Initialize(GameObject prefab, int sizePool)
        {
            _sizePool = sizePool;

            for (int i = 0; i < _sizePool; i++)
            {
                GameObject spawned = Instantiate(prefab, _container.transform);
                spawned.SetActive(false);
                _pool.Add(spawned);
            }
        }

        public bool TryGetObject(out GameObject result)
        {
            result = _pool.FirstOrDefault(p => p.activeSelf == false);

            return result != null;
        }

        public void ResetPool()
        {
            foreach (var item in _pool)
            {
                item.SetActive(false);
            }
        }
    }
}