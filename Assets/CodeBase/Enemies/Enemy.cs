using System.Collections;
using CodeBase.PlayerLogic;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _maxYOffset;
        [SerializeField] private float _minYOffset;
        [SerializeField] private float _maxMoveSpeed;
        [SerializeField] private float _minMoveSpeed;

        private Coroutine _moveRoutine;
        private Vector3 _initialPosition;

        private void OnEnable()
        {
            _initialPosition = transform.position;
            StartMove();
        }

        private void OnDisable()
        {
            StopMove();
        }
        
        private void OnBecameInvisible()
        {
            BackToPool();
        }

        public void StartMove()
        {
            StopMove();
            _moveRoutine = StartCoroutine(Move());
        }

        public void StopMove()
        {
            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Player playerSphere))
            {
                playerSphere.Kill();
            }
        }

        private void BackToPool()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator Move()
        {
            float verticalSpeed = Random.Range(_maxMoveSpeed,_minMoveSpeed);
            float maxY = _initialPosition.y + _maxYOffset;
            float minY = _initialPosition.y + _minYOffset;


            while (gameObject.activeSelf)
            {
                while (transform.position.y < maxY)
                {
                    transform.position += Vector3.up * (verticalSpeed * Time.deltaTime);
                    yield return null;
                }
                
                while (transform.position.y > minY)
                {
                    transform.position -= Vector3.up * (verticalSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }
}