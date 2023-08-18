using UnityEngine;

namespace CodeBase.Colision
{
    public class CollisionGround : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out PlayerLogic.Player playerSphere ))
            {
                playerSphere.Kill();
            }
        }
    }
}