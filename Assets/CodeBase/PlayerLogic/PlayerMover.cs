using System;
using UnityEngine;

namespace CodeBase.PlayerLogic
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private float _speed;
        [SerializeField] private float _tapForce;
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isInitialized;
        public event Action GameStart;

        private void Update()
        {
            if (_isInitialized == false)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                GameStart?.Invoke();
                _rigidbody.velocity = new Vector2(_speed, 0);
                _rigidbody.AddForce(Vector2.up * _tapForce, ForceMode.Force);
            }
        }

        public void Initialize()
        {
            transform.position = _startPosition;
            _rigidbody.velocity = Vector2.zero;
            _isInitialized = true;
        }
    }
}