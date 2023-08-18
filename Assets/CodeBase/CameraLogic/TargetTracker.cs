using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class TargetTracker : MonoBehaviour
    {
        [SerializeField] private float _xOffsetAxis;
        
        private Transform _target;

        private bool _isInitialized;

        private void Update()
        {
            if (_isInitialized == false)
                return;

            transform.position = new Vector3(_target.position.x - _xOffsetAxis,transform.position.y,transform.position.z);
        }

        public void Initialize(Transform target)
        {
            _target = target;
            _isInitialized = true;
        }
    }
}
